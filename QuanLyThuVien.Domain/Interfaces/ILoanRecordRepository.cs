using QuanLyThuVien.Domain.Entities;
using QuanLyThuVien.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Domain.Interfaces
{
    public interface ILoanRecordRepository : IGenericRepository<LoanRecord>
    {
        Task<IEnumerable<LoanRecord>> GetByUserAsync(int UserId);
        Task<LoanRecord?> GetActiveLoanAsync(int UserId, int BookId);
    }
}
