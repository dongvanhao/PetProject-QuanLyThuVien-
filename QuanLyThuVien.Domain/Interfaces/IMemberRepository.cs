using QuanLyThuVien.Domain.Entities;
using QuanLyThuVien.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Domain.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        IEnumerable<Member> GetMembersByName(string name);
        IEnumerable<Member> GetMembersByEmail(string email);
        IEnumerable<Member> GetMembersByPhone(string phone);
    }
}
