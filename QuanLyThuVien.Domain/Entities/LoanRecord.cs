using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Domain.Entities
{
    public class LoanRecord
    {
        public int LoanRecordId { get; set; }//Khoa chinh


        //Khoa lien ket voi book
        public int BookId { get; set; }
        public Book? Book { get; set; }

        //Khoa lien ket voi member
        public int MemberId { get; set; }
        public Member? Member { get; set; }

        //Thoi Gian Muon Va Tra
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public bool IsReturned { get; set; } = false;//Trang thai tra sach
    }
}
