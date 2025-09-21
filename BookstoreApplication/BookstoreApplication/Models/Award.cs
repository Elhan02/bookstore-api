namespace BookstoreApplication.Models
{
    public class Award
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartedAt { get; set; }

        public List<AuthorAward> AuthorAwards { get; set; }
    }
}
