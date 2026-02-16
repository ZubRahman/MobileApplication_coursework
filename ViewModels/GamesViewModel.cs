using System.Collections.ObjectModel;
using MyMauiApp.Models;
using MyMauiApp.Services.Interfaces;

namespace MyMauiApp.ViewModels;

public class GamesViewModel : BaseViewModel
{
    private readonly IGameCatalogService _gameService;

    public ObservableCollection<Game> Games { get; } = new();

    public GamesViewModel(IGameCatalogService gameService)
    {
        _gameService = gameService;

        // Fire-and-forget safely (no warning, no blocking constructor)
        _ = LoadGames();
    }

    private async Task LoadGames()
    {
        try
        {
            var games = await _gameService.GetGamesAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Games.Clear();
                foreach (var g in games)
                    Games.Add(g);
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }
    }
}
