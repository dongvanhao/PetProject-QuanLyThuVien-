using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuanLySinhVien.Application.Interfaces;
using QuanLyThuVien.Application.DTOs;

namespace QuanLySinhVien.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        
        private readonly IBookService _service;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService service, ILogger<BookController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword, int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("📘 [GET] /api/book - Tìm tất cả sách với keyword = {Keyword}", keyword);

            try
            {
                var books = await _service.GetAllAsync(keyword, page, pageSize);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi lấy danh sách sách");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("📖 [GET] /api/book/{id}", id);

            var book = await _service.GetByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning("⚠️ Không tìm thấy sách với id = {Id}", id);
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            var book = await _service.CreateAsync(dto);
            _logger.LogInformation("✅ [POST] Tạo sách mới: {Title}", dto.Title);

            return CreatedAtAction(nameof(GetById), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookDto dto)
        {
            if (id != dto.BookId)
            {
                _logger.LogWarning("⚠️ [PUT] Id không khớp: route = {RouteId}, body = {BodyId}", id, dto.BookId);
                return BadRequest();
            }

            var success = await _service.UpdateAsync(dto);
            if (!success)
            {
                _logger.LogWarning("⚠️ Không tìm thấy sách để cập nhật với id = {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("✏️ [PUT] Đã cập nhật sách với id = {Id}", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                _logger.LogWarning("⚠️ Không tìm thấy sách để xóa với id = {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("🗑️ [DELETE] Đã xóa sách với id = {Id}", id);
            return NoContent();
        }

        [HttpGet("filter-options")]
        public async Task<IActionResult> GetBooksByFilter([FromQuery] string field, [FromQuery] string? value)
        {
            _logger.LogInformation("🔍 [GET] /api/book/filter-options?field={Field}&value={Value}", field, value);

            var books = await _service.GetBooksByFieldAsync(field, value);
            return Ok(books);
        }

        [HttpGet("with-loans")]
        public async Task<IActionResult> GetBooksWithLoans()
        {
            _logger.LogInformation("📦 [GET] /api/book/with-loans");

            var books = await _service.GetBooksWithLoansAsync();
            return Ok(books);
        }

        [HttpGet("top-borrowed")]
        public async Task<IActionResult> GetTopBorrowedBooks([FromQuery] int top = 5)
        {
            _logger.LogInformation("🏆 [GET] /api/book/top-borrowed?top={Top}", top);

            var books = await _service.GetTopBorrowedBooksAsync(top);
            return Ok(books);
        }

        [HttpGet("with-loan-users")]
        public async Task<IActionResult> GetBooksWithLoanUsers()
        {
            _logger.LogInformation("👥 [GET] /api/book/with-loan-users");

            var books = await _service.GetBooksWithLoanUsersAsync();
            return Ok(books);
        }
    }
}
