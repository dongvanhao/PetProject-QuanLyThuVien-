using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Application.DTOs
{
    public class UpdateUserDto : CreateUserDto
    {
        public int UserId { get; set; }
    }
}
