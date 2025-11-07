using System.Text.Json.Serialization;

namespace BookstoreApplication.DTOs
{
    public class VineComicImageDto
    {

        [JsonPropertyName("small_url")]
        public string SmallUrl { get; set; }
    }
}
