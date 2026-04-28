using CalorieTrackerClient.Models;
using CalorieTrackerClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerClient.Services
{
    public class FoodItemService(HttpClient http) : IFoodItemService
    {
        private readonly HttpClient _http = http;

        public async Task<List<FoodItemDto>> SearchAsync(string searchText)
        {
            return await _http.GetFromJsonAsync<List<FoodItemDto>>(
                $"api/fooditem?search={Uri.EscapeDataString(searchText)}"
            ) ?? new();
        }

        public async Task<bool> CreateAsync(CreateFoodItemDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/fooditem", dto);
            return response.IsSuccessStatusCode;
        }
    }
}
