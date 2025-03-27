using AutoMapper;
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

        public LoanRecordService(ILoanRecordRepository loanRepo, IBookRepository bookRepo, IUserRepository userRepo, IMapper mapper)
        {
            _loanRepo = loanRepo;
            _bookRepo = bookRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<LoanRecordDto> BorrowBookAsync(CreateLoanRecordDto dto)
        {
            var book = await _bookRepo.GetByIdAsync(dto.BookId);
            if (book == null || book.AvailableCopies <= 0)
                throw new Exception("Sách không còn để mượn.");

            var existingLoan = await _loanRepo.GetActiveLoanAsync(dto.UserId, dto.BookId);
            if (existingLoan != null)
                throw new Exception("Người dùng đã mượn sách này.");

            var loan = new LoanRecord
            {
                BookId = dto.BookId,
                UserId = dto.UserId,
                LoanDate = DateTime.UtcNow
            };

            book.AvailableCopies -= 1;

            await _loanRepo.AddAsync(loan);
            await _bookRepo.UpdateAsync(book);

            return _mapper.Map<LoanRecordDto>(loan);
        }

        public async Task<bool> ReturnBookAsync(int loanRecordId)
        {
            var loan = await _loanRepo.GetByIdAsync(loanRecordId);
            if (loan == null || loan.ReturnDate != null)
                return false;

            loan.ReturnDate = DateTime.UtcNow;

            var book = await _bookRepo.GetByIdAsync(loan.BookId);
            if (book != null)
            {
                book.AvailableCopies += 1;
                await _bookRepo.UpdateAsync(book);
            }

            await _loanRepo.UpdateAsync(loan);
            return true;
        }

        public async Task<IEnumerable<LoanRecordDto>> GetUserHistoryAsync(int userId)
        {
            var records = await _loanRepo.GetByUserAsync(userId);
            return _mapper.Map<IEnumerable<LoanRecordDto>>(records);
        }
    }
}
