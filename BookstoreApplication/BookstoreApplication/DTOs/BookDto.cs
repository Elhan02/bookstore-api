using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public int PageCount { get; set; }

        [Required]
        public string AuthorFullName { get; set; }

        [Required]
        public string PublisherName { get; set; }

        [Range(0, int.MaxValue)]
        public int YearsOld { get; set; }
        
    }
}
