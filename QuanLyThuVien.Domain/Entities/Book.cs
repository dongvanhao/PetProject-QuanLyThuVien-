using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }//Khoa chinh
        public string Author { get; set; }//Tac gia
        public int Year { get; set; }   
        public int cost { get; set; }//Gia
        public string Genre { get; set;}//Loai
        public string ISBN { get; set; }//Ma ISBN
        public bool IsAvailable { get; set; }//Trang thai sach  

        public ICollection<LoanRecord>? LoanRecords { get; set; }//Quan he 1-n

    }
}
