using BookstoreApplication.Models;

namespace BookstoreApplication.Repositories
{
    public class IssuesRepository : IIssueRepository
    {
        private readonly AppDbContext _context;

        public IssuesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Issue> CreateAsync(Issue issue)
        {
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();
            return issue;
        }
    }
}
