using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Domain.Entities;
using QuanLyThuVien.Domain.Interfaces;
using QuanLyThuVien.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyThuVien.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Lấy danh sách sách theo tác giả (sử dụng Contains để tìm kiếm theo phần tên)
        public async Task<List<Book>> GetBooksByAuthor(string author)
        {
            return await _context.Books
                .Where(b => b.Author.Contains(author))
                .ToListAsync();
        }

        // Lấy danh sách sách theo thể loại (sử dụng Contains để tìm kiếm)
        public async Task<List<Book>> GetBooksByGenre(string genre)
        {
            return await _context.Books
                .Where(b => b.Genre.Contains(genre))
                .ToListAsync();
        }

        // Lấy danh sách sách theo giá (so sánh chính xác)
        public async Task<List<Book>> GetBooksByCost(int cost)
        {
            return await _context.Books
                .Where(b => b.Cost == cost)
                .ToListAsync();
        }

        // Lấy danh sách sách theo năm xuất bản
        public async Task<List<Book>> GetBooksByYear(int year)
        {
            return await _context.Books
                .Where(b => b.Year == year)
                .ToListAsync();
        }

        // Lấy danh sách sách theo trạng thái có sẵn (true nếu sách đang có sẵn)
        public async Task<List<Book>> GetBooksByAvailability(bool isAvailable)
        {
            return await _context.Books
                .Where(b => b.IsAvailable == isAvailable)
                .ToListAsync();
        }

        // Lấy sách theo phân trang: pageNumber là số trang, pageSize là số bản ghi mỗi trang
        public async Task<List<Book>> GetPagedBooksAsync(int pageNumber, int pageSize)
        {
            return await _context.Books
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
