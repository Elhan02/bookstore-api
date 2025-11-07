using System.Text.Json.Serialization;

namespace BookstoreApplication.DTOs
{
    public class IssueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonPropertyName("cover_date")]
        public string CoverDate { get; set; }

        [JsonPropertyName("api_detail_url")]
        public string ApiDetailUrl { get; set; }
        public VineComicImageDto Image { get; set; }
    }
}
