using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerClient.Models
{
    public class NutritionResponseDto
    {
        public double BMR { get; set; }
        public double TDEE { get; set; }
        public double DailyCalories { get; set; }
        public MacroTargetDto Macros { get; set; } = new();
    }

    public class MacroTargetDto
    {
        public double ProteinGrams { get; set; }
        public double CarbsGrams { get; set; }
        public double FatGrams { get; set; }
    }
}
