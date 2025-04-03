using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Application.Interfaces;
using QuanLySinhVien.Application.Services;
using QuanLySinhVien.Infrastructure.Repositories;
using QuanLyThuVien.API.Middleware;
using QuanLyThuVien.Application.Interfaces;
using QuanLyThuVien.Application.Mappings;
using QuanLyThuVien.Application.Services;
using QuanLyThuVien.Domain.Interfaces;
using QuanLyThuVien.Infrastructure.Data;
using QuanLyThuVien.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddLogging();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<ILoanRecordRepository, LoanRecordRepository>();
builder.Services.AddScoped<ILoanRecordService, LoanRecordService>();
var app = builder.Build();
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(dbContext);
}
app.Run();
