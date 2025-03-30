using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Domain.Entities;
using QuanLyThuVien.Domain.Interfaces;
using QuanLyThuVien.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Infrastructure.Repositories
{
    public class LoanRecordRepository : GenericRepository<LoanRecord>, ILoanRecordRepository
    {
        private readonly ApplicationDbContext _context;
        public LoanRecordRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LoanRecord>> GetByUserAsync(int userId)
        {
            return await _context.LoanRecords
                .AsNoTracking()//Truy vấn chỉ đọc, tránh tracking Book/User
                .Include(r => r.Book)
                .Include(r => r.User)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.LoanDate)
                .ToListAsync();
        }

        public async Task<LoanRecord?> GetActiveLoanAsync(int userId, int bookId)
        {
            return await _context.LoanRecords
                .FirstOrDefaultAsync(r => r.UserId == userId && r.BookId == bookId && r.ReturnDate == null);
        }
    }
}
