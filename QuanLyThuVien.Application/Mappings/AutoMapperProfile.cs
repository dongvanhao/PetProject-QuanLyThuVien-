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
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            CreateMap<LoanRecord, LoanRecordDto>();

            CreateMap<CreateLoanRecordDto, LoanRecord>();

            // Ánh xạ Book → BookDetailDto
            CreateMap<Book, BookDetailDto>();

            // Các ánh xạ khác nếu có
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();

        }
    }
}
