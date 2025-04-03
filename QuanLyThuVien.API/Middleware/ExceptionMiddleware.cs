using QuanLyThuVien.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace QuanLyThuVien.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Cho request đi tiếp
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Exception caught in middleware");//ghi log lỗi
                await HandleExceptionAsync(context, ex);//gửi lỗi qua HandleExceptionAsync
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new { message = ex.Message };
            _logger.LogError(ex, "Đã xử lý lỗi tại middleware: {Message}", ex.Message);

            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                ConflictException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }

}
