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
    }
}
