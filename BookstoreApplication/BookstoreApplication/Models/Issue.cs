namespace BookstoreApplication.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CoverDate { get; set; }
        public int IssueNumber { get; set; }
        public string ImageUrl { get; set; }
        public int ApiId { get; set; }
        public int PageNumber { get; set; }
        public int Price { get; set; }
        public int AvailableCopies { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    }
}
