using CalorieTrackerClient.Models;
using CalorieTrackerClient.Services.Interfaces;
using System.Text.Json;

namespace CalorieTrackerClient.Services
{
    public class FoodItemService : IFoodItemService
    {
        private readonly HttpClient _http;
        private readonly IApiService _apiService;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public FoodItemService(HttpClient http, IApiService apiService)
        {
            _http = http;
            _apiService = apiService;
        }

        public async Task<List<FoodItemDto>> SearchAsync(string searchText)
        {
            var response = await _http.GetAsync(
                $"api/fooditem?search={Uri.EscapeDataString(searchText)}"
            );

            if (!response.IsSuccessStatusCode)
                return new();

            var raw = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<FoodItemDto>>(raw, _jsonOptions) ?? new();
        }

        public async Task<bool> CreateAsync(CreateFoodItemDto dto)
        {
            var response = await _apiService.SendJsonAsync(
                HttpMethod.Post,
                "api/fooditem",
                dto
            );

            return response.IsSuccessStatusCode;
        }
    }
}