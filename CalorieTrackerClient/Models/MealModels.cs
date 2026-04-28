using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerClient.Models
{
    public class MealResponseDto
    {
        public int MealID { get; set; }
        public int SelectedMealType { get; set; }
        public DateTime Date { get; set; }
        public List<MealEntryResponseDto> MealEntries { get; set; } = new();
    }

    public class MealEntryResponseDto
    {
        public int MealEntryID { get; set; }
        public double QuantityInGrams { get; set; }
        public FoodItemDto FoodItem { get; set; } = new();
    }
}
