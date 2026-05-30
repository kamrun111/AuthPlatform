using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AuthPlatform.Mvc.Session;




namespace AuthPlatform.Mvc.Services
{


    public class ApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly TokenSessionManager _tokenSessionManager;

        public ApiClientService(
            HttpClient httpClient,
            IConfiguration configuration,
            TokenSessionManager tokenSessionManager)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _tokenSessionManager = tokenSessionManager;

            _httpClient.BaseAddress = new Uri(
                _configuration["ApiSettings:BaseUrl"]!);
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            AddBearerToken();

            var response = await _httpClient.GetAsync(endpoint);

            return await ReadResponse<T>(response);
        }

        public async Task<T?> PostAsync<T>(string endpoint, object data)
        {
            AddBearerToken();

            var json = JsonSerializer.Serialize(data);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            return await ReadResponse<T>(response);
        }

        public async Task<T?> PutAsync<T>(string endpoint, object data)
        {
            AddBearerToken();

            var json = JsonSerializer.Serialize(data);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);

            return await ReadResponse<T>(response);
        }

        public async Task<T?> DeleteAsync<T>(string endpoint)
        {
            AddBearerToken();

            var response = await _httpClient.DeleteAsync(endpoint);

            return await ReadResponse<T>(response);
        }

        private void AddBearerToken()
        {
            var token = _tokenSessionManager.GetAccessToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private static async Task<T?> ReadResponse<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }

            var trimmed = json.TrimStart();

            if (!trimmed.StartsWith("{") &&
                !trimmed.StartsWith("["))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}
