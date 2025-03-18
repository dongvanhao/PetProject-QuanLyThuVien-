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
        IEnumerable<LoanRecord> GetLoanRecordsByMemberId(int memberId);
        IEnumerable<LoanRecord> GetLoanRecordsByBookId(int bookId);
        IEnumerable<LoanRecord> GetLoanRecordsByDate(DateTime date);
        IEnumerable<LoanRecord> GetLoanRecordsByReturnDate(DateTime returnDate);
        IEnumerable<LoanRecord> GetLoanRecordsByStatus(bool status);
    }
}
