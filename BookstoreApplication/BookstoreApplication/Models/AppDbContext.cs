using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<AuthorAward> AuthorAwards { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Librarian", NormalizedName = "LIBRARIAN" },
                new IdentityRole { Name = "Editor", NormalizedName = "EDITOR" }
            );

            modelBuilder.Entity<AuthorAward>(entity =>
            {
                entity.ToTable("AuthorAwardBridge");

                entity.HasKey(aa => new { aa.AuthorId, aa.AwardId });

                entity.HasOne(aa => aa.Author)
                .WithMany(a => a.AuthorAwards)
                .HasForeignKey(aa => aa.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(aa => aa.Award)
                .WithMany(a => a.AuthorAwards)
                .HasForeignKey(aa => aa.AwardId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Author>()
                .Property(a => a.DateOfBirth)
                .HasColumnName("Birthday");

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany()
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FullName = "J.K. Rowling", Biography = "Author of Harry Potter", DateOfBirth = new DateTime(1965, 7, 31, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 2, FullName = "George R.R. Martin", Biography = "Author of A Song of Ice and Fire", DateOfBirth = new DateTime(1948, 9, 20, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 3, FullName = "Agatha Christie", Biography = "Queen of Mystery", DateOfBirth = new DateTime(1890, 9, 15, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 4, FullName = "Stephen King", Biography = "King of Horror", DateOfBirth = new DateTime(1947, 9, 21, 0, 0, 0, DateTimeKind.Utc) },
                new Author { Id = 5, FullName = "Isaac Asimov", Biography = "Science fiction writer", DateOfBirth = new DateTime(1920, 1, 2, 0, 0, 0, DateTimeKind.Utc) }
            );

            // Seed Publishers
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Penguin Random House", Address = "New York, USA", Website = "https://www.penguinrandomhouse.com" },
                new Publisher { Id = 2, Name = "HarperCollins", Address = "New York, USA", Website = "https://www.harpercollins.com" },
                new Publisher { Id = 3, Name = "Macmillan Publishers", Address = "London, UK", Website = "https://us.macmillan.com" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Harry Potter and the Sorcerer's Stone", PageCount = 309, PublishedDate = new DateTime(1997, 6, 26, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0439708180", AuthorId = 1, PublisherId = 1 },
                new Book { Id = 2, Title = "Harry Potter and the Chamber of Secrets", PageCount = 341, PublishedDate = new DateTime(1998, 7, 2, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0439064873", AuthorId = 1, PublisherId = 1 },
                new Book { Id = 3, Title = "A Game of Thrones", PageCount = 694, PublishedDate = new DateTime(1996, 8, 6, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0553103540", AuthorId = 2, PublisherId = 2 },
                new Book { Id = 4, Title = "A Clash of Kings", PageCount = 768, PublishedDate = new DateTime(1998, 11, 16, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0553108033", AuthorId = 2, PublisherId = 2 },
                new Book { Id = 5, Title = "Murder on the Orient Express", PageCount = 256, PublishedDate = new DateTime(1934, 1, 1, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0062693662", AuthorId = 3, PublisherId = 3 },
                new Book { Id = 6, Title = "And Then There Were None", PageCount = 272, PublishedDate = new DateTime(1939, 11, 6, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0062073488", AuthorId = 3, PublisherId = 3 },
                new Book { Id = 7, Title = "The Shining", PageCount = 447, PublishedDate = new DateTime(1977, 1, 28, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0307743657", AuthorId = 4, PublisherId = 1 },
                new Book { Id = 8, Title = "It", PageCount = 1138, PublishedDate = new DateTime(1986, 9, 15, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-1501142970", AuthorId = 4, PublisherId = 1 },
                new Book { Id = 9, Title = "Foundation", PageCount = 255, PublishedDate = new DateTime(1951, 6, 1, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0553293357", AuthorId = 5, PublisherId = 2 },
                new Book { Id = 10, Title = "I, Robot", PageCount = 224, PublishedDate = new DateTime(1950, 12, 2, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0553294385", AuthorId = 5, PublisherId = 2 },
                new Book { Id = 11, Title = "Carrie", PageCount = 199, PublishedDate = new DateTime(1974, 4, 5, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0307743664", AuthorId = 4, PublisherId = 1 },
                new Book { Id = 12, Title = "The Casual Vacancy", PageCount = 503, PublishedDate = new DateTime(2012, 9, 27, 0, 0, 0, DateTimeKind.Utc), ISBN = "978-0316228534", AuthorId = 1, PublisherId = 1 }
            );

            // Seed Awards
            modelBuilder.Entity<Award>().HasData(
                new Award { Id = 1, Name = "Pulitzer Prize", Description = "Award for achievements in journalism and literature.", StartedAt = new DateTime(1917, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Award { Id = 2, Name = "Hugo Award", Description = "Award for science fiction or fantasy works.", StartedAt = new DateTime(1953, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Award { Id = 3, Name = "Nebula Award", Description = "Annual award for the best science fiction or fantasy works.", StartedAt = new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Award { Id = 4, Name = "Booker Prize", Description = "Literary prize awarded each year for the best original novel.", StartedAt = new DateTime(1969, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            // Seed AuthorAward (join table)
            modelBuilder.Entity<AuthorAward>().HasData(
                new { AuthorId = 1, AwardId = 1, YearAwarded = 2001 },
                new { AuthorId = 1, AwardId = 4, YearAwarded = 2010 },
                new { AuthorId = 2, AwardId = 2, YearAwarded = 1998 },
                new { AuthorId = 2, AwardId = 3, YearAwarded = 2000 },
                new { AuthorId = 2, AwardId = 4, YearAwarded = 2012 },
                new { AuthorId = 3, AwardId = 1, YearAwarded = 1940 },
                new { AuthorId = 3, AwardId = 4, YearAwarded = 1950 },
                new { AuthorId = 4, AwardId = 1, YearAwarded = 1980 },
                new { AuthorId = 4, AwardId = 2, YearAwarded = 1985 },
                new { AuthorId = 4, AwardId = 3, YearAwarded = 1990 },
                new { AuthorId = 5, AwardId = 2, YearAwarded = 1960 },
                new { AuthorId = 5, AwardId = 3, YearAwarded = 1965 },
                new { AuthorId = 5, AwardId = 4, YearAwarded = 1970 },
                new { AuthorId = 1, AwardId = 2, YearAwarded = 2005 },
                new { AuthorId = 3, AwardId = 2, YearAwarded = 1939 }
            );
        }
    }
}
