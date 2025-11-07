using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using System.Text.Json;

namespace BookstoreApplication.Services
{
    public class IssueService : IIssueService
    {
        private readonly IConfiguration _configuration;
        private readonly IComicVineConnection _connection;
        private readonly IIssueRepository _repository;
        private readonly IMapper _mapper;

        public IssueService(IConfiguration configuration, IComicVineConnection connection, IIssueRepository repository, IMapper mapper)
        {
            _configuration = configuration;
            _connection = connection;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<IssueDto>> GetIssuesByVolume(int volumeId)
        {
            var url = $"{_configuration["ComicVine:BaseUrl"]}/issues" +
                $"?api_key={_configuration["ComicVine:APIKey"]}" +
                $"&format=json" +
                $"&filter=volume:{Uri.EscapeDataString(volumeId.ToString())}";

            var json = await _connection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<IssueDto>>(json, options);
        }

        public async Task<ApiCreateIssueDto> GetIssueByUrl(string apiDetailUrl)
        {
            var url = $"{apiDetailUrl.TrimEnd('/')}" +
                $"?api_key={_configuration["ComicVine:APIKey"]}" +
                $"&format=json";

            var json = await _connection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<ApiCreateIssueDto>(json, options);
        }

        public async Task<Issue> CreateAsync(CreateIssueDto dto)
        {
            ApiCreateIssueDto apiIssueDto = await GetIssueByUrl(dto.ApiDetailUrl);
            Issue issue = _mapper.Map<Issue>(apiIssueDto);
            _mapper.Map(dto, issue);
            issue.CoverDate = DateTime.SpecifyKind(issue.CoverDate, DateTimeKind.Utc);
            return await _repository.CreateAsync(issue);
        }
    }
}
