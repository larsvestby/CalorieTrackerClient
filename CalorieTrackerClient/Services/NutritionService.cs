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
    public class NutritionService(HttpClient http) : INutritionService
    {
        private readonly HttpClient _http = http;

        public async Task<NutritionResponseDto?> GetMineAsync()
        {
            return await _http.GetFromJsonAsync<NutritionResponseDto>("api/nutrition/me");
        }
    }
}
