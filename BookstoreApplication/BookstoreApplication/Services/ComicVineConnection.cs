using BookstoreApplication.Exceptions;
using System.Net;
using System.Text.Json;

namespace BookstoreApplication.Services
{
    public class ComicVineConnection : IComicVineConnection
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ComicVineConnection> _logger;

        public ComicVineConnection(HttpClient httpClient, ILogger<ComicVineConnection> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.ParseAdd("BookStoreApp");

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            JsonDocument jsonDocument = JsonDocument.Parse(json);

            if (!response.IsSuccessStatusCode)
            {
                HandleUnSuccessfulRequest(response, jsonDocument);
            }

            int statusCode = jsonDocument.RootElement.GetProperty("status_code").GetInt32();
            if (statusCode != 1)
            {
                HandleUnSuccessfulRequest(response, jsonDocument);
            }

            return jsonDocument.RootElement.GetProperty("results").GetRawText();
        }

        public void HandleUnSuccessfulRequest(HttpResponseMessage response, JsonDocument jsonDocument)
        {
            var errorMessage = "";
            try
            {
                errorMessage = jsonDocument.RootElement.GetProperty("error").GetString();
                _logger.LogError($"Request API failed: {(int)response.StatusCode} - {response.ReasonPhrase}: {errorMessage}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured with message: {ex.Message}");
            }

            if(response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new RateLimitException();
            }
            else if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedApiException();
            }
            else
            {
                string apiError = string.IsNullOrEmpty(errorMessage) ? "Error occured when sending request to the external API" : errorMessage;
                throw new ApiConnectionException(apiError);
            }
        }
    }
}
