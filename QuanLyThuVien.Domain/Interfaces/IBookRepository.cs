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
        IEnumerable<Book> GetBooksByAuthor(string author);
        IEnumerable<Book> GetBooksByGenre(string genre);
        IEnumerable<Book> GetBooksByCost(int cost);
        IEnumerable<Book> GetBooksByYear(int year);
        IEnumerable<Book> GetBooksByISBN(string isbn);
        IEnumerable<Book> GetBooksByAvailability(bool isAvailable);
    }
}
