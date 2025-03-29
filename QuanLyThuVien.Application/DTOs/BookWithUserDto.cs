using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Application.DTOs
{
    public class BookWithUserDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public List<UserLoanDto> LoanUsers { get; set; } = new();
    }
}
