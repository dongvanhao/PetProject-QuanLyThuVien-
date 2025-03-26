using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Application.DTOs
{
    public class BookDto
    {
        public int BookId { get; set; }//Khoa chinh
       
        public string Author { get; set; }//Tac gia
      
        public int Year { get; set; }

        public int Cost { get; set; }//Gia

        public string Genre { get; set; }//Loai

        public string ISBN { get; set; }//Ma ISBN

        public bool IsAvailable { get; set; } = true;//Trang thai sach 


    }
}
