
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
    }
}
