namespace BookstoreApplication.DTOs
{
    public class CreateIssueDto
    {
        public string ApiDetailUrl { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int PageNumber { get; set; }
        public int AvailableCopies { get; set; }
    }
}
