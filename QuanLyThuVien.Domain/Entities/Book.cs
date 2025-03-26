using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Domain.Entities
{
    [Table("Book")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }//Khoa chinh
        [Required]
        public string Author { get; set; }//Tac gia
        [Required]
        public int Year { get; set; }
        [Required]
        public int Cost { get; set; }//Gia
        [Required]
        public string Genre { get; set; }//Loai
        [Required]
        public string ISBN { get; set; }//Ma ISBN
        [Required]
        public bool IsAvailable { get; set; } = true;//Trang thai sach  

        public ICollection<LoanRecord>? LoanRecords { get; set; }//Quan he 1-n

    }
}
