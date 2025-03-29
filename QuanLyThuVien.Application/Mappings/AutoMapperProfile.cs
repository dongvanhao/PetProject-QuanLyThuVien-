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

            


            CreateMap<Book, BookSummaryDto>();


            CreateMap<Book, BookWithUserDto>()
            .ForMember(dest => dest.LoanUsers, opt => opt.MapFrom(src =>
            src.LoanRecords!.Select(lr => new UserLoanDto
             {
            LoanRecordId = lr.LoanRecordId,
            BorrowedDate = lr.LoanDate,
            FullName = lr.User.FullName
                }).ToList()
            ));

        }
    }
}
