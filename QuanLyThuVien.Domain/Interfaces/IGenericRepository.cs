using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Domain.Repositories
{
    // 'T' là kiểu dữ liệu tổng quát, đảm bảo T phải là một lớp (class)
    public interface IGenericRepository<T> where T : class // Giới hạn T phải là kiểu tham chiếu (class) để Entity Framework có thể làm việc
    {
        //Lay tat ca cac ban ghi cua T
        Task<IEnumerable<T>> GetAllAsync();
        //Lay ban ghi cua T theo id
        Task<T> GetByIdAsync(int id);
        //Them ban ghi cua T
        Task<T> AddAsync(T entity);
        //Cap nhat ban ghi cua T
        Task UpdateAsync(T entity);
        //Xoa ban ghi cua T
        Task DeleteAsync(int id);

    }
}
