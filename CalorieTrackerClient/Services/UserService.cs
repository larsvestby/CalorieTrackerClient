using System.Text;
using System.Text.Json;
using CalorieTrackerClient.Models;
using CalorieTrackerClient.Services.Interfaces;

namespace CalorieTrackerClient.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        private readonly IApiService _apiService;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public UserService(HttpClient http, IApiService apiService)
        {
            _http = http;
            _apiService = apiService;
        }

        public async Task<UserResponseDto?> GetMeAsync()
        {
            var response = await _http.GetAsync("api/user/me");

            if (!response.IsSuccessStatusCode)
                return null;

            var raw = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UserResponseDto>(raw, _jsonOptions);
        }

        public async Task<bool> UpdateMeAsync(UpdateUserDto dto)
        {
            var response = await _apiService.SendJsonAsync(
                HttpMethod.Put,
                "api/user/me",
                dto
            );

            return response.IsSuccessStatusCode;
        }
    }
}