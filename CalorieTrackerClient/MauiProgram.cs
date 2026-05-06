using CalorieTrackerClient.Services;
using CalorieTrackerClient.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;

namespace CalorieTrackerClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if ANDROID
            builder.Services.AddScoped(sp =>
                new HttpClient { BaseAddress = new Uri("https://lars.buchwaldshave34.dk/") });
#else
            builder.Services.AddScoped(sp =>
                new HttpClient { BaseAddress = new Uri("https://localhost:7072/") });
#endif
            builder.Services.AddScoped<IApiService, ApiService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<IFoodItemService, FoodItemService>();
            builder.Services.AddScoped<IMealService, MealService>();
            builder.Services.AddScoped<INutritionService, NutritionService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<JwtAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
                sp.GetRequiredService<JwtAuthStateProvider>());


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
