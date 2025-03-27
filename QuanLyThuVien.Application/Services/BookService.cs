using AutoMapper;

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
    }
}
