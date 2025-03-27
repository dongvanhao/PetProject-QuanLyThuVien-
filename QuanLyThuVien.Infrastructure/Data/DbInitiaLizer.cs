using QuanLyThuVien.Domain.Entities;

namespace QuanLyThuVien.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any() || context.Books.Any() || context.LoanRecords.Any())
                return;

            // Seed USERS
            var users = new List<User>();
            for (int i = 1; i <= 10; i++)
            {
                users.Add(new User
                {
                    FullName = $"Người dùng {i}",
                    Email = $"user{i}@example.com"
                });
            }
            context.Users.AddRange(users);
            context.SaveChanges();

            // Seed BOOKS
            var books = new List<Book>();
            for (int i = 1; i <= 15; i++)
            {
                books.Add(new Book
                {
                    Title = $"Sách {i}",
                    Author = $"Tác giả {i}",
                    Year = 2015 + (i % 5),
                    Cost = 90000 + i * 5000,
                    Genre = (i % 2 == 0) ? "Giáo khoa" : "Tham khảo",
                    ISBN = $"ISBN-{1000 + i}",
                    TotalCopies = 10,
                    AvailableCopies = 10
                });
            }
            context.Books.AddRange(books);
            context.SaveChanges();

            // Seed LOAN RECORDS
            var rand = new Random();
            var loans = new List<LoanRecord>();
            for (int i = 0; i < 30; i++)
            {
                var user = users[rand.Next(users.Count)];
                var book = books[rand.Next(books.Count)];

                if (book.AvailableCopies > 0)
                {
                    book.AvailableCopies -= 1;

                    var loan = new LoanRecord
                    {
                        BookId = book.BookId,
                        UserId = user.UserId,
                        LoanDate = DateTime.UtcNow.AddDays(-rand.Next(1, 30)),
                        ReturnDate = (i % 4 == 0) ? DateTime.UtcNow.AddDays(-rand.Next(1, 10)) : null
                    };

                    loans.Add(loan);
                }
            }

            context.LoanRecords.AddRange(loans);
            context.SaveChanges();
        }
    }
}
