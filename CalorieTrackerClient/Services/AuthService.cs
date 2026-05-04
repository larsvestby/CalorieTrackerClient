using System.Text.Json;
using CalorieTrackerClient.Models;
using CalorieTrackerClient.Services.Interfaces;

namespace CalorieTrackerClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiService _apiService;

        private const string TokenKey = "auth_token";

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public AuthService(HttpClient httpClient, IApiService apiService)
        {
            _httpClient = httpClient;
            _apiService = apiService;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var payload = new
            {
                email,
                password
            };

            var response = await _apiService.SendJsonAsync(
                HttpMethod.Post,
                "api/auth/login",
                payload
            );

            return await HandleAuthResponse(response);
        }

        public async Task<AuthResult> RegisterAsync(RegisterRequest request)
        {
            var response = await _apiService.SendJsonAsync(
                HttpMethod.Post,
                "api/auth/register",
                request
            );

            return await HandleAuthResponse(response);
        }

        private async Task<AuthResult> HandleAuthResponse(HttpResponseMessage response)
        {
            var raw = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = raw
                };
            }

            var authResponse = JsonSerializer.Deserialize<AuthResponse>(raw, _jsonOptions);

            if (authResponse?.Token == null)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "No token returned."
                };
            }

            await StoreTokenAsync(authResponse.Token);
            SetAuthHeader(authResponse.Token);

            return new AuthResult
            {
                Success = true,
                Token = authResponse.Token,
                User = authResponse.User
            };
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
    }
}