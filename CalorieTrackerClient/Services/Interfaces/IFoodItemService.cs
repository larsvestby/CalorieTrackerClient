using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalorieTrackerClient.Models;

namespace CalorieTrackerClient.Services.Interfaces
{
    public interface IFoodItemService
    {
        Task<List<FoodItemDto>> SearchAsync(string searchText);
        Task<bool> CreateAsync(CreateFoodItemDto dto);
    }
}
