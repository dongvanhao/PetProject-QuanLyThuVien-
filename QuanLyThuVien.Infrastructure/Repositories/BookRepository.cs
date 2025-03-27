using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Domain.Entities;
using QuanLyThuVien.Domain.Interfaces;
using QuanLyThuVien.Infrastructure.Data;
using QuanLyThuVien.Infrastructure.Repositories;

namespace QuanLySinhVien.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> SearchAsync(string? keyword, int page, int pageSize)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(b =>
                    b.Title.Contains(keyword) ||
                    b.Genre.Contains(keyword) ||
                    b.Author.Contains(keyword)); // nếu có thuộc tính Author
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<int> CountAsync(string? keyword)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(b => b.Title.Contains(keyword) || b.Genre.Contains(keyword));
            }

            return await query.CountAsync();
        }
    }
}
