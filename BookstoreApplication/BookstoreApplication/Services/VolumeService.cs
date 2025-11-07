using BookstoreApplication.DTOs;
using System.Text.Json;

namespace BookstoreApplication.Services
{
    public class VolumeService : IVolumeService
    {
        private readonly IConfiguration _configuration;
        private readonly IComicVineConnection _connection;

        public VolumeService(IConfiguration configuration, IComicVineConnection connection)
        {
            _configuration = configuration;
            _connection = connection;
        }

        public async Task<List<VolumeDto>> SearchVolumesByName(string filter)
        {
            var url = $"{_configuration["ComicVine:BaseUrl"]}/volumes" +
                $"?api_key={_configuration["ComicVine:APIKey"]}" +
                $"&format=json" +
                $"&filter=name:{Uri.EscapeDataString(filter)}";

            var json = await _connection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<VolumeDto>>(json, options);
        }
    }
}
