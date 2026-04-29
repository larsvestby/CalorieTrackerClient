using CalorieTrackerClient.Models;

namespace CalorieTrackerClient.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto?> GetMeAsync();
        Task<bool> UpdateMeAsync(UpdateUserDto dto);
    }
}