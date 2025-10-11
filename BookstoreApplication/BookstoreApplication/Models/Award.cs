using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.Models
{
    public class Award
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        public List<AuthorAward> AuthorAwards { get; set; }
    }
}
