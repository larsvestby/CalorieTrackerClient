using System.Net;
using System.Text;
using System.Text.Json;
using CalorieTrackerClient.Services.Interfaces;

namespace CalorieTrackerClient.Services
{
    /// <summary>
    /// whole file made using CHATGPT
    /// </summary>
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> SendJsonAsync<T>(
            HttpMethod method,
            string url,
            T payload)
        {
            var json = JsonSerializer.Serialize(payload, JsonOptions);

            using var request = new HttpRequestMessage(method, url)
            {
                Version = HttpVersion.Version11,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            request.Headers.ExpectContinue = false;

            return await _httpClient.SendAsync(request);
        }

        public async Task<TResponse?> ReadJsonAsync<TResponse>(HttpResponseMessage response)
        {
            var raw = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(raw, JsonOptions);
        }

        public async Task<string> ReadRawAsync(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }
    }
}