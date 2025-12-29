using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BookstoreApplication.Repositories
{
    public class IssuesNoSqlRepository : IIssueRepository
    {
        private readonly IMongoCollection<Issue> _issuesCollection;

        public IssuesNoSqlRepository(IMongoCollection<Issue> issuesCollection)
        {
            _issuesCollection = issuesCollection;
        }

        public async Task<Issue> CreateAsync(Issue issue)
        {
            await _issuesCollection.InsertOneAsync(issue);
            return issue;
        }
    }
}
