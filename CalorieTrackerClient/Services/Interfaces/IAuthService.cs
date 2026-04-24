using CalorieTrackerClient.Models;

namespace CalorieTrackerClient.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> RegisterAsync(RegisterRequest request);
        Task LogoutAsync();
        Task<string?> GetStoredTokenAsync();

        List<ActivityLevelDto> GetActivityLevels();
        List<GoalDto> GetGoals();
    }
}