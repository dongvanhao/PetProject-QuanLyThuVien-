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
        Task<IEnumerable<Book>> SearchAsync(string? keyword, int page, int pageSize);
        Task<int> CountAsync(string? keyword);
        Task<List<Book>> GetBooksWithLoanRecordsAsync();

        //top sach 5 dc muon nhieu nhat
        Task<List<Book>> GetTopBorrowedBooksAsync(int top);

        //Lấy danh sách sách cùng với người đã mượn
        Task<List<Book>> GetBooksWithLoanUsersAsync();

    }
}
