using BookstoreApplication.Models;

namespace BookstoreApplication.DTOs
{
    public class PublisherSortTypeOptionDto
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public PublisherSortTypeOptionDto(PublisherSortType publisherSortType)
        {
            Key = (int)publisherSortType;
            Name = publisherSortType.ToString();
        }
    }
}
