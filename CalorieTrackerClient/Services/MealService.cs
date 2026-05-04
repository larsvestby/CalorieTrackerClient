using System.Text.Json;
using CalorieTrackerClient.Models;
using CalorieTrackerClient.Services.Interfaces;

namespace CalorieTrackerClient.Services
{
    public class MealService : IMealService
    {
        private readonly HttpClient _http;
        private readonly IApiService _apiService;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public MealService(HttpClient http, IApiService apiService)
        {
            _http = http;
            _apiService = apiService;
        }

        public async Task<List<MealResponseDto>> GetByDateAsync(DateTime date)
        {
            var response = await _http.GetAsync($"api/meal?date={date:yyyy-MM-dd}");

            if (!response.IsSuccessStatusCode)
                return new();

            var raw = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<MealResponseDto>>(raw, _jsonOptions) ?? new();
        }

        public async Task<MealResponseDto?> CreateAsync(DateTime date, int selectedMealType)
        {
            var response = await _apiService.SendJsonAsync(
                HttpMethod.Post,
                "api/meal",
                new
                {
                    Date = date,
                    SelectedMealType = selectedMealType
                }
            );

            if (!response.IsSuccessStatusCode)
                return null;

            var raw = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<MealResponseDto>(raw, _jsonOptions);
        }

        public async Task<bool> AddEntryAsync(int mealId, int foodItemId, double quantityInGrams)
        {
            var response = await _apiService.SendJsonAsync(
                HttpMethod.Post,
                $"api/meal/{mealId}/entries",
                new
                {
                    FoodItemID = foodItemId,
                    QuantityInGrams = quantityInGrams
                }
            );

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveEntryAsync(int mealEntryId)
        {
            var response = await _http.DeleteAsync($"api/meal/entries/{mealEntryId}");
            return response.IsSuccessStatusCode;
        }
    }
}