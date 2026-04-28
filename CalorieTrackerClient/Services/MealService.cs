using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using CalorieTrackerClient.Models;
using CalorieTrackerClient.Services.Interfaces;

namespace CalorieTrackerClient.Services
{
    public class MealService(HttpClient http) : IMealService
    {
        private readonly HttpClient _http = http;

        public async Task<List<MealResponseDto>> GetByDateAsync(DateTime date)
        {
            return await _http.GetFromJsonAsync<List<MealResponseDto>>(
                $"api/meal?date={date:yyyy-MM-dd}"
            ) ?? new();
        }

        public async Task<MealResponseDto?> CreateAsync(DateTime date, int selectedMealType)
        {
            var response = await _http.PostAsJsonAsync("api/meal", new
            {
                Date = date,
                SelectedMealType = selectedMealType
            });

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<MealResponseDto>();
        }

        public async Task<bool> AddEntryAsync(int mealId, int foodItemId, double quantityInGrams)
        {
            var response = await _http.PostAsJsonAsync($"api/meal/{mealId}/entries", new
            {
                FoodItemID = foodItemId,
                QuantityInGrams = quantityInGrams
            });

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveEntryAsync(int mealEntryId)
        {
            var response = await _http.DeleteAsync($"api/meal/entries/{mealEntryId}");
            return response.IsSuccessStatusCode;
        }
    }
}
