using CalorieTrackerClient.Models;

namespace CalorieTrackerClient.Models
{
    public class UserResponseDto
    {
        public int UserID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public Gender SelectedGender { get; set; }
        public int ActivityLevelID { get; set; }
        public string ActivityLevelName { get; set; } = "";
        public int GoalID { get; set; }
        public string GoalName { get; set; } = "";
    }

    public class UpdateUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public Gender? SelectedGender { get; set; }
        public int? ActivityLevelID { get; set; }
        public int? GoalID { get; set; }
    }
}