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
        public DbSet<QuanLyThuVien.Domain.Entities.Book> Books { get; set; }
        public DbSet<QuanLyThuVien.Domain.Entities.Member> Members { get; set; }
        public DbSet<QuanLyThuVien.Domain.Entities.LoanRecord> LoanRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanRecord>()
               .HasOne(l => l.Book)
               .WithMany(b => b.LoanRecords)
               .HasForeignKey(l => l.BookId)
               .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình quan hệ LoanRecord - Member
            modelBuilder.Entity<LoanRecord>()
                .HasOne(l => l.Member)
                .WithMany(m => m.LoanRecords)
                .HasForeignKey(l => l.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
