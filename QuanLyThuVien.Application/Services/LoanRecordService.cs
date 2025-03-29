using AutoMapper;
using Microsoft.Extensions.Logging;
using QuanLyThuVien.Application.DTOs;
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
            if (book == null || book.AvailableCopies <= 0)
            {
                _logger.LogWarning("⚠️ [BorrowBook] Sách không có sẵn hoặc không tồn tại: BookId = {BookId}", dto.BookId);
                throw new Exception("Sách không còn để mượn.");
            }

            var existingLoan = await _loanRepo.GetActiveLoanAsync(dto.UserId, dto.BookId);
            if (existingLoan != null)
            {
                _logger.LogWarning("⚠️ [BorrowBook] Người dùng đã mượn sách này: UserId = {UserId}, BookId = {BookId}", dto.UserId, dto.BookId);
                throw new Exception("Người dùng đã mượn sách này.");
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

            _logger.LogInformation("✅ [BorrowBook] Đã mượn thành công: UserId = {UserId}, BookId = {BookId}", dto.UserId, dto.BookId);
            return _mapper.Map<LoanRecordDto>(loan);
        }

        public async Task<bool> ReturnBookAsync(int loanRecordId)
        {
            _logger.LogInformation("📤 [ReturnBook] Trả sách với LoanRecordId = {LoanId}", loanRecordId);

            var loan = await _loanRepo.GetByIdAsync(loanRecordId);
            if (loan == null || loan.ReturnDate != null)
            {
                _logger.LogWarning("⚠️ [ReturnBook] Không thể trả sách. Bản ghi không hợp lệ hoặc đã được trả: LoanId = {LoanId}", loanRecordId);
                return false;
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

            var records = await _loanRepo.GetByUserAsync(userId);
            return _mapper.Map<IEnumerable<LoanRecordDto>>(records);
        }
    }
}
