using AutoMapper;
using QuanLyThuVien.Application.DTOs;
using QuanLyThuVien.Application.Interfaces;
using QuanLyThuVien.Domain.Entities;
using QuanLyThuVien.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyThuVien.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        // Lấy danh sách tất cả sách, map từ entity Book sang BookDto
        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        // Lấy sách theo ID, nếu không tìm thấy ném ngoại lệ
        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException($"Book with id {id} not found.");
            return _mapper.Map<BookDto>(book);
        }

        // Thêm mới sách: chuyển CreateBookDto sang Book, lưu và map kết quả sang BookDto
        public async Task<BookDto> AddBookAsync(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);
            var createdBook = await _bookRepository.AddAsync(book);
            return _mapper.Map<BookDto>(createdBook);
        }

        // Cập nhật sách: lấy sách theo ID, cập nhật các thuộc tính từ UpdateBookDto, sau đó lưu thay đổi
        public async Task UpdateBookAsync(UpdateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetByIdAsync(updateBookDto.BookId);
            if (book == null)
                throw new KeyNotFoundException($"Book with id {updateBookDto.BookId} not found.");

            // Map các thuộc tính cập nhật từ DTO sang entity hiện có
            _mapper.Map(updateBookDto, book);
            await _bookRepository.UpdateAsync(book);
        }

        // Xóa sách theo ID
        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        // Lấy sách theo tác giả (theo tên tác giả hoặc chứa chuỗi tìm kiếm)
        public async Task<List<BookDto>> GetBooksByAuthor(string author)
        {
            var books = await _bookRepository.GetBooksByAuthor(author);
            return _mapper.Map<List<BookDto>>(books);
        }

        // Lấy sách theo thể loại
        public async Task<List<BookDto>> GetBooksByGenre(string genre)
        {
            var books = await _bookRepository.GetBooksByGenre(genre);
            return _mapper.Map<List<BookDto>>(books);
        }

        // Lấy sách theo giá
        public async Task<List<BookDto>> GetBooksByCost(int cost)
        {
            var books = await _bookRepository.GetBooksByCost(cost);
            return _mapper.Map<List<BookDto>>(books);
        }

        // Lấy sách theo năm xuất bản
        public async Task<List<BookDto>> GetBooksByYear(int year)
        {
            var books = await _bookRepository.GetBooksByYear(year);
            return _mapper.Map<List<BookDto>>(books);
        }

        // Lấy sách theo trạng thái có sẵn
        public async Task<List<BookDto>> GetBooksByAvailability(bool isAvailable)
        {
            var books = await _bookRepository.GetBooksByAvailability(isAvailable);
            return _mapper.Map<List<BookDto>>(books);
        }

        // Lấy sách theo phân trang
        public async Task<List<BookDto>> GetPagedBooksAsync(int pageNumber, int pageSize)
        {
            var books = await _bookRepository.GetPagedBooksAsync(pageNumber, pageSize);
            return _mapper.Map<List<BookDto>>(books);
        }
    }
}
