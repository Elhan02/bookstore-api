using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IIssueService
    {
        Task<List<IssueDto>> GetIssuesByVolume(int volumeId);
        Task<Issue> CreateAsync(CreateIssueDto dto);
    }
}
