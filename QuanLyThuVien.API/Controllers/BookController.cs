using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.Application.Interfaces;
using QuanLySinhVien.Application.Services;
using QuanLyThuVien.Application.DTOs;

namespace QuanLySinhVien.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                var books = await _service.GetAllAsync(keyword, page, pageSize);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            var book = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookDto dto)
        {
            if (id != dto.BookId) return BadRequest();
            var success = await _service.UpdateAsync(dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        //Test
        [HttpGet("filter-options")]
        public async Task<IActionResult> GetBooksByFilter([FromQuery] string field, [FromQuery] string? value)
        {
            var books = await _service.GetBooksByFieldAsync(field, value);
            return Ok(books);
        }

        [HttpGet("with-loans")]
        public async Task<IActionResult> GetBooksWithLoans()
        {
            var books = await _service.GetBooksWithLoansAsync();
            return Ok(books);
        }

        [HttpGet("top-borrowed")]
        public async Task<IActionResult> GetTopBorrowedBooks([FromQuery] int top = 5)
        {
            var books = await _service.GetTopBorrowedBooksAsync(top);
            return Ok(books);
        }


        [HttpGet("with-loan-users")]
        public async Task<IActionResult> GetBooksWithLoanUsers()
        {
            var books = await _service.GetBooksWithLoanUsersAsync();
            return Ok(books);
        }


    }
}
