using AutoMapper;
using Microsoft.Extensions.Logging;
using QuanLyThuVien.Application.DTOs;
using QuanLyThuVien.Application.Exceptions;
using QuanLyThuVien.Application.Interfaces;
using QuanLyThuVien.Domain.Entities;
using QuanLyThuVien.Domain.Interfaces;

namespace QuanLyThuVien.Application.Services
{
    public class LoanRecordService : ILoanRecordService
    {
        private readonly ILoanRecordRepository _loanRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<LoanRecordService> _logger;

        public LoanRecordService(
            ILoanRecordRepository loanRepo,
            IBookRepository bookRepo,
            IUserRepository userRepo,
            IMapper mapper,
            ILogger<LoanRecordService> logger)
        {
            _loanRepo = loanRepo;
            _bookRepo = bookRepo;
            _userRepo = userRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LoanRecordDto> BorrowBookAsync(CreateLoanRecordDto dto)
        {
            _logger.LogInformation("📥 [BorrowBook] Yêu cầu mượn sách: UserId = {UserId}, BookId = {BookId}", dto.UserId, dto.BookId);

            var book = await _bookRepo.GetByIdAsync(dto.BookId);
            if (book == null)
            {
                _logger.LogWarning("⚠️ [BorrowBook] Không tìm thấy sách: BookId = {BookId}", dto.BookId);
                throw new NotFoundException($"Không tìm thấy sách với ID = {dto.BookId}");
            }

            if (book.AvailableCopies <= 0)
            {
                _logger.LogWarning("⚠️ [BorrowBook] Sách không có sẵn: BookId = {BookId}", dto.BookId);
                throw new ConflictException("Sách hiện không có sẵn để mượn.");
            }

            var existingLoan = await _loanRepo.GetActiveLoanAsync(dto.UserId, dto.BookId);
            if (existingLoan != null)
            {
                _logger.LogWarning("⚠️ [BorrowBook] Người dùng đã mượn sách này: UserId = {UserId}, BookId = {BookId}", dto.UserId, dto.BookId);
                throw new ConflictException("Người dùng đã mượn sách này rồi.");
            }

            var loan = new LoanRecord
            {
                BookId = dto.BookId,
                UserId = dto.UserId,
                LoanDate = DateTime.UtcNow
            };

            book.AvailableCopies -= 1;

            await _loanRepo.AddAsync(loan);
            await _bookRepo.UpdateAsync(book);

            _logger.LogInformation("✅ [BorrowBook] Mượn thành công: UserId = {UserId}, BookId = {BookId}", dto.UserId, dto.BookId);
            return _mapper.Map<LoanRecordDto>(loan);
        }


        public async Task<bool> ReturnBookAsync(int loanRecordId)
        {
            _logger.LogInformation("📤 [ReturnBook] Trả sách với LoanRecordId = {LoanId}", loanRecordId);

            var loan = await _loanRepo.GetByIdAsync(loanRecordId);

            if (loan == null)
            {
                _logger.LogWarning("❌ [ReturnBook] Không tìm thấy bản ghi mượn: LoanId = {LoanId}", loanRecordId);
                throw new NotFoundException($"Không tìm thấy bản ghi mượn sách với ID = {loanRecordId}");
            }

            if (loan.ReturnDate != null)
            {
                _logger.LogWarning("⚠️ [ReturnBook] Sách đã được trả trước đó: LoanId = {LoanId}", loanRecordId);
                throw new ConflictException("Sách này đã được trả trước đó.");
            }

            loan.ReturnDate = DateTime.UtcNow;

            var book = await _bookRepo.GetByIdAsync(loan.BookId);
            if (book != null)
            {
                book.AvailableCopies += 1;
                await _bookRepo.UpdateAsync(book);
            }

            await _loanRepo.UpdateAsync(loan);

            _logger.LogInformation("✅ [ReturnBook] Đã trả sách thành công: LoanId = {LoanId}", loanRecordId);
            return true;
        }


        public async Task<IEnumerable<LoanRecordDto>> GetUserHistoryAsync(int userId)
        {
            _logger.LogInformation("📚 [GetUserHistory] Lấy lịch sử mượn sách cho người dùng ID = {UserId}", userId);

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("⚠️ [GetUserHistory] Không tìm thấy người dùng với ID = {UserId}", userId);
                throw new NotFoundException($"Không tìm thấy người dùng với ID = {userId}");
            }

            var records = await _loanRepo.GetByUserAsync(userId);

            if (!records.Any())
            {
                _logger.LogInformation("ℹ️ [GetUserHistory] Người dùng ID = {UserId} không có lịch sử mượn sách.", userId);
            }
            else
            {
                _logger.LogInformation("✅ [GetUserHistory] Tìm thấy {Count} bản ghi cho người dùng ID = {UserId}", records.Count(), userId);
            }

            return _mapper.Map<IEnumerable<LoanRecordDto>>(records);
        }

    }
}
