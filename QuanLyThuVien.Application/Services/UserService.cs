using AutoMapper;
using Microsoft.Extensions.Logging;
using QuanLyThuVien.Application.DTOs;
using QuanLyThuVien.Application.Interfaces;
using QuanLyThuVien.Domain.Entities;
using QuanLyThuVien.Domain.Interfaces;

namespace QuanLyThuVien.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository repository, IMapper mapper, ILogger<UserService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            try
            {
                var user = _mapper.Map<User>(dto);
                await _repository.AddAsync(user);
                _logger.LogInformation("✅ [CreateUser] Đã tạo người dùng: {Email}", user.Email);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ [CreateUser] Lỗi khi tạo người dùng");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var user = await _repository.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("⚠️ [DeleteUser] Không tìm thấy người dùng ID = {Id}", id);
                    return false;
                }

                await _repository.DeleteAsync(id);
                _logger.LogInformation("🗑️ [DeleteUser] Đã xóa người dùng ID = {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ [DeleteUser] Lỗi khi xóa người dùng ID = {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            try
            {
                var users = await _repository.GetAllAsync();
                _logger.LogInformation("📋 [GetAllUsers] Đã lấy danh sách người dùng ({Count})", users.Count());
                return _mapper.Map<IEnumerable<UserDto>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ [GetAllUsers] Lỗi khi lấy danh sách người dùng");
                throw;
            }
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            try
            {
                var user = await _repository.GetByEmailAsync(email);
                if (user == null)
                {
                    _logger.LogWarning("⚠️ [GetUserByEmail] Không tìm thấy người dùng với email = {Email}", email);
                    return null;
                }

                _logger.LogInformation("📧 [GetUserByEmail] Tìm thấy người dùng với email = {Email}", email);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ [GetUserByEmail] Lỗi khi tìm người dùng email = {Email}", email);
                throw;
            }
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            try
            {
                var user = await _repository.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("⚠️ [GetUserById] Không tìm thấy người dùng với ID = {Id}", id);
                    return null;
                }

                _logger.LogInformation("👤 [GetUserById] Tìm thấy người dùng với ID = {Id}", id);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ [GetUserById] Lỗi khi tìm người dùng ID = {Id}", id);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(UpdateUserDto dto)
        {
            try
            {
                var user = await _repository.GetByIdAsync(dto.UserId);
                if (user == null)
                {
                    _logger.LogWarning("⚠️ [UpdateUser] Không tìm thấy người dùng để cập nhật ID = {Id}", dto.UserId);
                    return false;
                }

                _mapper.Map(dto, user);
                await _repository.UpdateAsync(user);

                _logger.LogInformation("✏️ [UpdateUser] Đã cập nhật người dùng ID = {Id}", dto.UserId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ [UpdateUser] Lỗi khi cập nhật người dùng ID = {Id}", dto.UserId);
                throw;
            }
        }
    }
}
