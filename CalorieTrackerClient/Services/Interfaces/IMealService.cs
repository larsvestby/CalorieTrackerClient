using CalorieTrackerClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerClient.Services.Interfaces
{
    public interface IMealService
    {
        Task<List<MealResponseDto>> GetByDateAsync(DateTime date);
        Task<MealResponseDto?> CreateAsync(DateTime date, int selectedMealType);
        Task<bool> AddEntryAsync(int mealId, int foodItemId, double quantityInGrams);
        Task<bool> RemoveEntryAsync(int mealEntryId);
    }
}
