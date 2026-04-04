using MyMauiApp.ViewModels;
using MyMauiApp.Views.Pages;
using MyMauiApp.Services.Interfaces;
using MyMauiApp.Services;
using Microsoft.Extensions.Logging;
using MyMauiApp.Data;
using MyMauiApp.Data.Repositories;

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
		// viewModels
		builder.Services.AddTransient<GamesViewModel>();
		builder.Services.AddTransient<DiaryViewModel>();
		builder.Services.AddTransient<ProfileViewModel>();
		builder.Services.AddTransient<AuthViewModel>();
		builder.Services.AddTransient<GameDetailsViewModel>();
		builder.Services.AddTransient<EditRatingViewModel>();
		builder.Services.AddTransient<StatsViewModel>();
		builder.Services.AddTransient<SyncViewModel>();

		// pages
		builder.Services.AddTransient<GamesPage>();
		builder.Services.AddTransient<DiaryPage>();
		builder.Services.AddTransient<ProfilePage>();
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<GameDetailsPage>();
		builder.Services.AddTransient<EditRatingPage>();
		builder.Services.AddTransient<DiaryEntryDetailsViewModel>();
		builder.Services.AddTransient<DiaryEntryDetailsPage>();
		builder.Services.AddTransient<SignUpPage>();
		builder.Services.AddTransient<StatsViewModel>();
		builder.Services.AddTransient<StatsPage>();
		

		builder.Services.AddSingleton<AppDb>();
		builder.Services.AddSingleton<IDiaryRepository, DiaryRepository>();
		

		builder.Services.AddSingleton<ISupabaseService, SupabaseService>();
		

		// services
		builder.Services.AddSingleton<IGameCatalogService, GameCatalogService>();
		return builder.Build();
	}
}
