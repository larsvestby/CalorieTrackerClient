using System.Text.Json.Serialization;

namespace CalorieTrackerClient.Models
{
    public enum Gender
    {
        Male = 0,
        Female = 1
    }

    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public Gender SelectedGender { get; set; }
        public int ActivityLevelID { get; set; }
        public int GoalID { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserResponse User { get; set; } = new();
    }

    public class UserResponse
    {
        public int UserID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public Gender SelectedGender { get; set; }
        public string ActivityLevel { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public UserResponse? User { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class ActivityLevelDto
    {
        public int ActivityLevelID { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Multiplier { get; set; }

        public static List<ActivityLevelDto> GetAll() =>
        [
            new() { ActivityLevelID = 1, Name = "Sedentary",         Multiplier = 1.2   },
            new() { ActivityLevelID = 2, Name = "Lightly active",    Multiplier = 1.375 },
            new() { ActivityLevelID = 3, Name = "Moderately active", Multiplier = 1.55  },
            new() { ActivityLevelID = 4, Name = "Highly active",     Multiplier = 1.725 },
            new() { ActivityLevelID = 5, Name = "Extremely active",  Multiplier = 1.9   },
        ];
    }

    public class GoalDto
    {
        public int GoalID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CalorieAdjustment { get; set; }

        public static List<GoalDto> GetAll() =>
        [
            new() { GoalID = 1, Name = "Weightloss",     CalorieAdjustment = -500 },
            new() { GoalID = 2, Name = "Maintain",       CalorieAdjustment = 0    },
            new() { GoalID = 3, Name = "Musclebuilding", CalorieAdjustment = 300  },
        ];
    }
}