
using QuanLyThuVien.Application.DTOs;

namespace QuanLySinhVien.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync(string? keyword, int page, int pageSize);
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto> CreateAsync(CreateBookDto dto);
        Task<bool> UpdateAsync(UpdateBookDto dto);
        Task<bool> DeleteAsync(int id);

        //Test
        Task<List<BookDetailDto>> GetBooksByFieldAsync(string field, string? value);
        //Top sach dc muon nhieu nhat
        Task<List<BookDetailDto>> GetBooksWithLoansAsync();

        //Top sach 5 dc muon nhieu nhat
        Task<List<BookSummaryDto>> GetTopBorrowedBooksAsync(int top);
        //Lấy danh sách sách cùng với người đã mượn
        Task<List<BookWithUserDto>> GetBooksWithLoanUsersAsync();

    }
}
