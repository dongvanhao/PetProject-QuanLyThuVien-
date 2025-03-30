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
            var query = _context.Books.AsNoTracking();//AsNoTracking giúp truy vấn nhẹ,chỉ đọc dữ liệu

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(b =>
                    b.Title.Contains(keyword) ||
                    b.Genre.Contains(keyword) ||
                    b.Author.Contains(keyword));
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync(string? keyword)
        {
            var query = _context.Books.AsNoTracking(); //Truy vấn tổng hợp

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(b => b.Title.Contains(keyword) || b.Genre.Contains(keyword));
            }

            return await query.CountAsync();
        }

        public async Task<List<Book>> GetBooksWithLoanRecordsAsync()
        {
            return await _context.Books
                .AsNoTracking()//Tránh track list LoanRecords
                .Include(b => b.LoanRecords)
                .ToListAsync();
        }

        public async Task<List<Book>> GetTopBorrowedBooksAsync(int top)
        {
            return await _context.Books
                .AsNoTracking()//Tránh track trong thống kê
                .OrderByDescending(b => b.LoanRecords.Count)
                .Take(top)
                .ToListAsync();
        }

        public async Task<List<Book>> GetBooksWithLoanUsersAsync()
        {
            return await _context.Books
                .AsNoTracking()// truy vấn sâu, cần tối ưu RAM
                .Include(b => b.LoanRecords!)
                    .ThenInclude(lr => lr.User)
                .ToListAsync();
        }
    }
}
