using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Application.DTOs
{
    public  class LoanRecordDto
    {
        public int LoanRecordId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookGenre { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
