using System.Net.Http.Json;
using System.Text.Json;
using CalorieTrackerClient.Models;
using CalorieTrackerClient.Services.Interfaces;

namespace CalorieTrackerClient.Services
{
    public class AuthService(HttpClient httpClient) : IAuthService
    {
        private readonly HttpClient _httpClient = httpClient;
        private const string TokenKey = "auth_token";

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var payload = new { email, password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", payload);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<AuthResponse>(_jsonOptions);
                if (data?.Token != null)
                {
                    await StoreTokenAsync(data.Token);
                    SetAuthHeader(data.Token);
                    return new AuthResult { Success = true, Token = data.Token, User = data.User };
                }
            }

            return new AuthResult { Success = false, ErrorMessage = await ParseErrorAsync(response) };
        }

        public async Task<AuthResult> RegisterAsync(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", request, _jsonOptions);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<AuthResponse>(_jsonOptions);
                if (data?.Token != null)
                {
                    await StoreTokenAsync(data.Token);
                    SetAuthHeader(data.Token);
                    return new AuthResult { Success = true, Token = data.Token, User = data.User };
                }
            }

            return new AuthResult { Success = false, ErrorMessage = await ParseErrorAsync(response) };
        }

        public List<ActivityLevelDto> GetActivityLevels() => ActivityLevelDto.GetAll();
        public List<GoalDto> GetGoals() => GoalDto.GetAll();

        public Task LogoutAsync()
        {
            SecureStorage.Default.Remove(TokenKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            return Task.CompletedTask;
        }

        public async Task<string?> GetStoredTokenAsync()
        {
            return await SecureStorage.Default.GetAsync(TokenKey);
        }

        private void SetAuthHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        private static async Task StoreTokenAsync(string token)
        {
            await SecureStorage.Default.SetAsync(TokenKey, token);
        }

        private static async Task<string> ParseErrorAsync(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine($"Error body: {body}");

            if (!string.IsNullOrWhiteSpace(body))
                return body;

            return response.StatusCode == System.Net.HttpStatusCode.Unauthorized
                ? "Invalid email or password."
                : "An error occurred. Please try again.";
        }
    }
}