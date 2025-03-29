using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository repository, IMapper mapper, ILogger<BookService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            try
            {
                var book = _mapper.Map<Book>(dto);
                book.AvailableCopies = book.TotalCopies;

                await _repository.AddAsync(book);
                _logger.LogInformation("✅ Đã tạo sách mới: {Title}", book.Title);

                return _mapper.Map<BookDto>(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi tạo sách");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var book = await _repository.GetByIdAsync(id);
                if (book == null)
                {
                    _logger.LogWarning("⚠️ Không tìm thấy sách với ID = {Id} để xóa", id);
                    return false;
                }

                await _repository.DeleteAsync(id);
                _logger.LogInformation("🗑️ Đã xóa sách có ID = {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi xóa sách ID = {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync(string? keyword, int page, int pageSize)
        {
            try
            {
                var books = await _repository.SearchAsync(keyword, page, pageSize);
                _logger.LogInformation("📚 Đã lấy danh sách sách với từ khóa '{Keyword}'", keyword);
                return _mapper.Map<IEnumerable<BookDto>>(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi lấy danh sách sách");
                throw;
            }
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            try
            {
                var book = await _repository.GetByIdAsync(id);
                if (book == null)
                {
                    _logger.LogWarning("⚠️ Không tìm thấy sách với ID = {Id}", id);
                    return null;
                }

                _logger.LogInformation("📖 Đã tìm thấy sách: {Title}", book.Title);
                return _mapper.Map<BookDto>(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi lấy sách theo ID = {Id}", id);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(UpdateBookDto dto)
        {
            try
            {
                var book = await _repository.GetByIdAsync(dto.BookId);
                if (book == null)
                {
                    _logger.LogWarning("⚠️ Không tìm thấy sách để cập nhật, ID = {Id}", dto.BookId);
                    return false;
                }

                _mapper.Map(dto, book);
                await _repository.UpdateAsync(book);

                _logger.LogInformation("✏️ Đã cập nhật sách ID = {Id}", dto.BookId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi cập nhật sách ID = {Id}", dto.BookId);
                throw;
            }
        }

        public async Task<List<BookDetailDto>> GetBooksByFieldAsync(string field, string? value)
        {
            try
            {
                _logger.LogInformation("🔍 Đang lọc sách theo trường '{Field}' với giá trị '{Value}'", field, value);

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
                            _logger.LogWarning("⚠️ Trường lọc không hợp lệ: {Field}", field);
                            return new List<BookDetailDto>();
                    }
                }

                var books = await query.ToListAsync();
                return _mapper.Map<List<BookDetailDto>>(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi lọc sách");
                throw;
            }
        }

        public async Task<List<BookDetailDto>> GetBooksWithLoansAsync()
        {
            try
            {
                var books = await _repository.GetBooksWithLoanRecordsAsync();
                _logger.LogInformation("📦 Lấy danh sách sách kèm theo lượt mượn");
                return _mapper.Map<List<BookDetailDto>>(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi lấy danh sách sách với lượt mượn");
                throw;
            }
        }

        public async Task<List<BookSummaryDto>> GetTopBorrowedBooksAsync(int top)
        {
            try
            {
                var books = await _repository.GetTopBorrowedBooksAsync(top);
                _logger.LogInformation("🏆 Lấy Top {Top} sách được mượn nhiều nhất", top);
                return _mapper.Map<List<BookSummaryDto>>(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi lấy top sách được mượn");
                throw;
            }
        }

        public async Task<List<BookWithUserDto>> GetBooksWithLoanUsersAsync()
        {
            try
            {
                var books = await _repository.GetBooksWithLoanUsersAsync();
                _logger.LogInformation("📖 Lấy sách và người mượn sách");
                return _mapper.Map<List<BookWithUserDto>>(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi lấy sách và người mượn");
                throw;
            }
        }
    }
}
