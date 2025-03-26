using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Domain.Entities
{
    [Table("LoanRecord")]
    public class LoanRecord
    {
        [ForeignKey("LoanRecord")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanRecordId { get; set; }//Khoa chinh


        //Khoa lien ket voi book
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book? Book { get; set; }

        //Khoa lien ket voi member
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        //Thoi Gian Muon Va Tra
        public DateTime LoanDate { get; set; }
        // Thời gian trả sách. Có thể để null nếu sách chưa được trả.
        public DateTime? ReturnDate { get; set; }
        // Trạng thái trả sách: true nếu sách đã được trả
        public bool IsReturned { get; set; } = false;//Trang thai tra sach
    }
}
