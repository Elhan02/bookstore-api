using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.DTOs
{
    public class BookDetailsDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Range(0, int.MaxValue)]
        public int PageCount { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Range(0, int.MaxValue)]
        public int AuthorId { get; set; }

        [Required]
        public string AuthorFullName { get; set; }

        [Range(0, int.MaxValue)]
        public int PublisherId { get; set; }

        [Required]
        public string PublisherName { get; set; }


    }
}
