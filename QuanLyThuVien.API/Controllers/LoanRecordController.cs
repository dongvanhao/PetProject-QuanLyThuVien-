using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuanLyThuVien.Application.DTOs;
using QuanLyThuVien.Application.Interfaces;

namespace QuanLyThuVien.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRecordController : ControllerBase
    {
        private readonly ILoanRecordService _service;
        private readonly ILogger<LoanRecordController> _logger;

        public LoanRecordController(ILoanRecordService service, ILogger<LoanRecordController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Mượn sách
        /// </summary>
        [HttpPost("borrow")]
        public async Task<IActionResult> Borrow([FromBody] CreateLoanRecordDto dto)
        {
            _logger.LogInformation("📥 [POST] /borrow - Yêu cầu mượn sách: UserId = {UserId}, BookId = {BookId}", dto.UserId, dto.BookId);

            try
            {
                var loan = await _service.BorrowBookAsync(dto);
                _logger.LogInformation("✅ Mượn sách thành công: LoanId = {LoanId}, UserId = {UserId}, BookId = {BookId}",
                    loan.LoanRecordId, loan.UserId, loan.BookId);
                return Ok(loan);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("⚠️ Mượn sách thất bại: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Trả sách
        /// </summary>
        [HttpPost("return/{id}")]
        public async Task<IActionResult> Return(int id)
        {
            _logger.LogInformation("📤 [POST] /return/{id} - Trả sách với LoanRecordId = {LoanId}", id);

            var result = await _service.ReturnBookAsync(id);
            if (!result)
            {
                _logger.LogWarning("⚠️ Trả sách thất bại hoặc bản ghi không tồn tại: LoanId = {LoanId}", id);
                return NotFound(new { message = "Không tìm thấy bản ghi hoặc sách đã được trả." });
            }

            _logger.LogInformation("✅ Trả sách thành công: LoanRecordId = {LoanId}", id);
            return Ok(new { message = "Trả sách thành công." });
        }

        /// <summary>
        /// Lịch sử mượn sách của người dùng
        /// </summary>
        [HttpGet("history/{userId}")]
        public async Task<IActionResult> History(int userId)
        {
            _logger.LogInformation("📚 [GET] /history/{userId} - Lấy lịch sử mượn sách cho UserId = {UserId}", userId);

            var history = await _service.GetUserHistoryAsync(userId);

            if (!history.Any())
            {
                _logger.LogWarning("⚠️ Không có lịch sử mượn sách cho UserId = {UserId}", userId);
            }
            else
            {
                _logger.LogInformation("✅ Lấy được {Count} bản ghi mượn sách cho UserId = {UserId}", history.Count(), userId);
            }

            return Ok(history);
        }
    }
}
