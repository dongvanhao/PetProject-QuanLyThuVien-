using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Domain.Repositories;
using QuanLyThuVien.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //Dbset de truy xuat du lieu
        protected readonly ApplicationDbContext _context;

        //Dbset tuong ung voi T (moi entity co 1 Dbset rieng)
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()//Lay tat ca cac ban ghi cua T
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)//Lay ban ghi cua T theo id
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            var addedEntity = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return addedEntity.Entity; // Trả về entity đã được cập nhật
        }



        public async Task UpdateAsync(T entity)//Cap nhat ban ghi cua T
        {
            _dbSet.Update(entity);//Danh dau entity da thay doi
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)//Xoa ban ghi cua T
        {
            // Tìm entity theo id
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }
}
