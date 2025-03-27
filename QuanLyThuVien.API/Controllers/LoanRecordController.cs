using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Application.DTOs;
using QuanLyThuVien.Application.Interfaces;

namespace QuanLyThuVien.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRecordController : ControllerBase
    {
        private readonly ILoanRecordService _service;

        public LoanRecordController(ILoanRecordService service)
        {
            _service = service;
        }

        /// <summary>
        /// Mượn sách
        /// </summary>
        [HttpPost("borrow")]
        public async Task<IActionResult> Borrow([FromBody] CreateLoanRecordDto dto)
        {
            try
            {
                var loan = await _service.BorrowBookAsync(dto);
                return Ok(loan);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Trả sách
        /// </summary>
        [HttpPost("return/{id}")]
        public async Task<IActionResult> Return(int id)
        {
            var result = await _service.ReturnBookAsync(id);
            if (!result) return NotFound(new { message = "Không tìm thấy bản ghi hoặc sách đã được trả." });

            return Ok(new { message = "Trả sách thành công." });
        }

        /// <summary>
        /// Lịch sử mượn sách của người dùng
        /// </summary>
        [HttpGet("history/{userId}")]
        public async Task<IActionResult> History(int userId)
        {
            var history = await _service.GetUserHistoryAsync(userId);
            return Ok(history);
        }
    }
}
