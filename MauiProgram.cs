using MyMauiApp.ViewModels;
using MyMauiApp.Views.Pages;
using MyMauiApp.Services.Interfaces;
using MyMauiApp.Services;

using Microsoft.Extensions.Logging;

namespace MyMauiApp;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		// ViewModels
		builder.Services.AddTransient<GamesViewModel>();
		builder.Services.AddTransient<DiaryViewModel>();
		builder.Services.AddTransient<ProfileViewModel>();
		builder.Services.AddTransient<AuthViewModel>();
		builder.Services.AddTransient<GameDetailsViewModel>();
		builder.Services.AddTransient<EditRatingViewModel>();
		builder.Services.AddTransient<StatsViewModel>();
		builder.Services.AddTransient<SyncViewModel>();

		// Pages
		builder.Services.AddTransient<GamesPage>();
		builder.Services.AddTransient<DiaryPage>();
		builder.Services.AddTransient<ProfilePage>();
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<GameDetailsPage>();
		builder.Services.AddTransient<EditRatingPage>();

		// Services
		builder.Services.AddSingleton<IGameCatalogService, GameCatalogService>();
		return builder.Build();
	}
}
