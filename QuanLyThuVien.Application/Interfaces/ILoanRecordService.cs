using QuanLyThuVien.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Application.Interfaces
{
    public interface ILoanRecordService
    {
        Task<LoanRecordDto> BorrowBookAsync(CreateLoanRecordDto dto);
        Task<bool> ReturnBookAsync(int loanRecordId);
        Task<IEnumerable<LoanRecordDto>> GetUserHistoryAsync(int userId);
    }
}
