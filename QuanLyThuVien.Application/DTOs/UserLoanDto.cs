using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Application.DTOs
{
    public class UserLoanDto
    {
        public int LoanRecordId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}
