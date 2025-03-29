using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Application.DTOs;
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

        //lay danh sach luot muon cua sach
        public async Task<List<Book>> GetBooksWithLoanRecordsAsync()
        {
            return await _context.Books
                .Include(b => b.LoanRecords)
                .ToListAsync();
        }


        //top sach 5 dc muon nhieu nhat
        public async Task<List<Book>> GetTopBorrowedBooksAsync(int top)
        {
            return await _context.Books
                .OrderByDescending(b => b.LoanRecords.Count)
                .Take(top)
                .ToListAsync();
        }
        //Lấy danh sách sách cùng với người đã mượn
        public async Task<List<Book>> GetBooksWithLoanUsersAsync()
        {
            return await _context.Books
                .Include(b => b.LoanRecords!)
                    .ThenInclude(lr => lr.User)
                .ToListAsync();
        }

    }
}
