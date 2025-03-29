using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Application.DTOs
{
    public class BookSummaryDto
    {
        public int BookId { get; set; } // ID sách

        public string Title { get; set; } = string.Empty; // Tên sách

        public string Author { get; set; } = string.Empty; // Tác giả

        public int Year { get; set; } // Năm xuất bản

        public string Genre { get; set; } = string.Empty; // Thể loại

        public string ISBN { get; set; } = string.Empty; // Mã ISBN

        public int AvailableCopies { get; set; } // Số lượng còn lại
    }
}
