using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.DTOs
{
    public class CreateBookReviewDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }
        public string? Comment { get; set; }
    }
}
