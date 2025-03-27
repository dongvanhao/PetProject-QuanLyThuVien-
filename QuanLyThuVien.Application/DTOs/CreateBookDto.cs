using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Application.DTOs
{
    public class CreateBookDto
    {

        public string Author { get; set; }//Tac gia

        public int Year { get; set; }

        public string Cost { get; set; }//Gia

        public string Genre { get; set; }//Loai

        public string ISBN { get; set; }//Ma ISBN

        public int TotalCopies { get; set; }
    }
}
