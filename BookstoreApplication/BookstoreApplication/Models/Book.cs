using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Range(10, int.MaxValue)]
        public int PageCount { get; set; }

        [Required]
        public required DateTime PublishedDate { get; set; }

        [Required]
        public required string ISBN { get; set; }

        [Range(0, int.MaxValue)]
        [Required]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        [Range(0, int.MaxValue)]
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        public double AverageRating { get; set; }
    }
}
