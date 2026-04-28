using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerClient.Models
{
    public class FoodItemDto
    {
        public int FoodItemID { get; set; }
        public string Name { get; set; } = "";
        public string? Brand { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbohydrates { get; set; }
        public double Fat { get; set; }
    }

    public class CreateFoodItemDto
    {
        public string Name { get; set; } = "";
        public string? Brand { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbohydrates { get; set; }
        public double Fat { get; set; }
    }
}
