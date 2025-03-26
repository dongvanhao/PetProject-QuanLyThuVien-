using QuanLyThuVien.Domain.Entities;
using QuanLyThuVien.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Domain.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<List<Book>> GetBooksByAuthor(string author);//Lay sach theo tac gia
        Task<List<Book>> GetBooksByGenre(string genre);//Lay sach theo the loai
        Task<List<Book>> GetBooksByCost(int cost);//Lay sach theo gia
        Task<List<Book>> GetBooksByYear(int year);//Lay sach theo nam xuat ban
        Task<List<Book>> GetBooksByAvailability(bool isAvailable);//Lay sach theo trang thai co san
        Task<List<Book>> GetPagedBooksAsync(int pageNumber, int pageSize);
    }
}
