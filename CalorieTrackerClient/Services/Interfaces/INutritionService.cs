using CalorieTrackerClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTrackerClient.Services.Interfaces
{
    public interface INutritionService
    {
        Task<NutritionResponseDto?> GetMineAsync();
    }
}
