using System.Net.Http.Json;
using CalorieTrackerClient.Models;
using CalorieTrackerClient.Services.Interfaces;

namespace CalorieTrackerClient.Services
{
    public class UserService(HttpClient http) : IUserService
    {
        private readonly HttpClient _http = http;

        public async Task<UserResponseDto?> GetMeAsync()
        {
            return await _http.GetFromJsonAsync<UserResponseDto>("api/user/me");
        }

        public async Task<bool> UpdateMeAsync(UpdateUserDto dto)
        {
            var response = await _http.PutAsJsonAsync("api/user/me", dto);
            return response.IsSuccessStatusCode;
        }
    }
}