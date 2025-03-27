using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Domain.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; } // Khóa chính

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty; // ⭐ Bạn nên thêm Tên sách

        [Required]
        [StringLength(255)]
        public string Author { get; set; } = string.Empty; // Tác giả

        [Required]
        [Range(1000, 2100)]
        public int Year { get; set; } // Năm xuất bản

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; } // ⭐ Dùng decimal cho giá

        [Required]
        [StringLength(100)]
        public string Genre { get; set; } = string.Empty; // Thể loại

        [Required]
        [StringLength(50)]
        public string ISBN { get; set; } = string.Empty; // Mã ISBN

        [Required]
        [Range(0, int.MaxValue)]
        public int TotalCopies { get; set; } // Tổng số lượng

        [Required]
        [Range(0, int.MaxValue)]
        public int AvailableCopies { get; set; } // Số còn lại

        // Quan hệ 1-n: 1 sách có nhiều lượt mượn
        public ICollection<LoanRecord>? LoanRecords { get; set; }
    }
}
