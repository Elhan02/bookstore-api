using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.Models
{
    public class BookReview
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public ApplicationUser? User { get; set; }
        public Book? Book { get; set; }

        [Range(1, 5)]
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

    }
}
