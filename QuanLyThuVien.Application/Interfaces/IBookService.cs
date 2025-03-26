using QuanLyThuVien.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyThuVien.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetByIdAsync(int id);
        Task<BookDto> AddBookAsync(CreateBookDto bookDto);
        Task UpdateBookAsync(UpdateBookDto bookDto);
        Task DeleteBookAsync(int id);
        Task<List<BookDto>> GetBooksByAuthor(string author);
        Task<List<BookDto>> GetBooksByGenre(string genre);
        Task<List<BookDto>> GetBooksByCost(int cost);
        Task<List<BookDto>> GetBooksByYear(int year);
        Task<List<BookDto>> GetBooksByAvailability(bool isAvailable);
        Task<List<BookDto>> GetPagedBooksAsync(int pageNumber, int pageSize);
    }
}
