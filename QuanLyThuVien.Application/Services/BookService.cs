using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Application.Interfaces;

using QuanLyThuVien.Application.DTOs;
using QuanLyThuVien.Domain.Entities;
using QuanLyThuVien.Domain.Interfaces;

namespace QuanLySinhVien.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            book.AvailableCopies = book.TotalCopies;

            await _repository.AddAsync(book);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync(string? keyword, int page, int pageSize)
        {
            var books = await _repository.SearchAsync(keyword, page, pageSize);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<bool> UpdateAsync(UpdateBookDto dto)
        {
            var book = await _repository.GetByIdAsync(dto.BookId);
            if (book == null) return false;

            _mapper.Map(dto, book);
            await _repository.UpdateAsync(book);
            return true;
        }

        //Test
        public async Task<List<BookDetailDto>> GetBooksByFieldAsync(string field, string? value)
        {
            var query = _repository.GetAllQueryable();

            if (!string.IsNullOrEmpty(value))
            {
                switch (field.ToLower())
                {
                    case "title":
                        query = query.Where(b => b.Title == value);
                        break;
                    case "author":
                        query = query.Where(b => b.Author == value);
                        break;
                    case "genre":
                        query = query.Where(b => b.Genre == value);
                        break;
                    case "year":
                        if (int.TryParse(value, out var year))
                            query = query.Where(b => b.Year == year);
                        break;
                    default:
                        return new List<BookDetailDto>();
                }
            }

            var books = await query.ToListAsync();
            return _mapper.Map<List<BookDetailDto>>(books);
        }


    }
}
