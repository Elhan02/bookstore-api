using System.Text.Json.Serialization;

namespace BookstoreApplication.DTOs
{
    public class ApiCreateIssueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("cover_date")]
        public string CoverDate { get; set; }

        [JsonPropertyName("issue_number")]
        public string IssueNumber { get; set; }
        public VineComicImageDto Image { get; set; }
    }
}
