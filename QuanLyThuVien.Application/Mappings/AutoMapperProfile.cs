using AutoMapper;
using QuanLyThuVien.Application.DTOs;
using QuanLyThuVien.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>()
            .ForMember(dest => dest.BookId, opt => opt.Ignore()); // ✅ Không map ID từ CreateBookDto
            CreateMap<UpdateBookDto, Book>();
        }
    }
}
