using System.Text.Json;
using CalorieTrackerClient.Models;
using CalorieTrackerClient.Services.Interfaces;

namespace CalorieTrackerClient.Services
{
    public class NutritionService : INutritionService
    {
        private readonly HttpClient _http;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public NutritionService(HttpClient http)
        {
            _http = http;
        }

        public async Task<NutritionResponseDto?> GetMineAsync()
        {
            var response = await _http.GetAsync("api/nutrition/me");

            if (!response.IsSuccessStatusCode)
                return null;

            var raw = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<NutritionResponseDto>(raw, _jsonOptions);
        }
    }
}