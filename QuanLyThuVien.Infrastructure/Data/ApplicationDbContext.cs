using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("YourConnectionString",
                    b => b.MigrationsAssembly("QuanLyThuVien.Infrastructure"));
            }
        }
        public DbSet<QuanLyThuVien.Domain.Entities.Book> Books { get; set; }
        public DbSet<QuanLyThuVien.Domain.Entities.User> Users { get; set; }
        public DbSet<QuanLyThuVien.Domain.Entities.LoanRecord> LoanRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cấu hình quan hệ LoanRecord - Book
            modelBuilder.Entity<LoanRecord>()
               .HasOne(l => l.Book)
               .WithMany(b => b.LoanRecords)
               .HasForeignKey(l => l.BookId)
               .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình quan hệ LoanRecord - User
            modelBuilder.Entity<LoanRecord>()
                .HasOne(l => l.User)
                .WithMany(m => m.LoanRecords)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
