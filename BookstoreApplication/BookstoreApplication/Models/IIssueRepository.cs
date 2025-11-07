namespace BookstoreApplication.Models
{
    public interface IIssueRepository
    {
        Task<Issue> CreateAsync(Issue issue);
    }
}
