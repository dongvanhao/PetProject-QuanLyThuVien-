using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuanLyThuVien.Application.DTOs;
using QuanLyThuVien.Application.Interfaces;

namespace QuanLyThuVien.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("📋 [GET] /api/user - Lấy danh sách người dùng");

            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("👤 [GET] /api/user/{id}", id);

            var user = await _service.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("⚠️ Không tìm thấy người dùng với ID = {Id}", id);
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("by-email")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            _logger.LogInformation("📧 [GET] /api/user/by-email?email={Email}", email);

            var user = await _service.GetByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("⚠️ Không tìm thấy người dùng với email = {Email}", email);
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            _logger.LogInformation("➕ [POST] /api/user - Tạo người dùng mới: {Email}", dto.Email);

            var user = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        {
            if (id != dto.UserId)
            {
                _logger.LogWarning("⚠️ [PUT] ID không khớp: route = {RouteId}, body = {BodyId}", id, dto.UserId);
                return BadRequest();
            }

            var result = await _service.UpdateAsync(dto);
            if (!result)
            {
                _logger.LogWarning("⚠️ Không tìm thấy người dùng để cập nhật với ID = {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("✏️ [PUT] Cập nhật người dùng với ID = {Id}", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                _logger.LogWarning("⚠️ Không tìm thấy người dùng để xóa với ID = {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("🗑️ [DELETE] Đã xóa người dùng với ID = {Id}", id);
            return NoContent();
        }
    }
}
