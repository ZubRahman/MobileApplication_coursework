using System.Collections.ObjectModel;
using System.Linq;
using MyMauiApp.Models;
using MyMauiApp.Services.Interfaces;
using System.Windows.Input;

namespace MyMauiApp.ViewModels;

public class GamesViewModel : BaseViewModel
{
    private readonly IGameCatalogService _gameService;

    public ObservableCollection<Game> Games { get; } = new();
    private List<Game> _allGames = new();

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value) return;
            _searchText = value;
            FilterGames();
        }
    }

    public GamesViewModel(IGameCatalogService gameService)
    {
        _gameService = gameService;
        OpenDetailsCommand = new Command<Game>(async (game) => await OpenDetails(game));
        _ = LoadGames();
    }

    public ICommand OpenDetailsCommand { get; }

    private async Task OpenDetails(Game? game)
{
    if (game is null) return;

    System.Diagnostics.Debug.WriteLine($"Navigating with ID: {game.Id}");

    await Shell.Current.GoToAsync($"gamedetails?id={Uri.EscapeDataString(game.Id)}");
}

    private async Task LoadGames()
    {
        try
        {
            var games = await _gameService.GetGamesAsync();
            _allGames = games.ToList();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Games.Clear();
                foreach (var g in _allGames) Games.Add(g);
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }
    }

    private void FilterGames()
    {
        var query = SearchText?.Trim();

        IEnumerable<Game> filtered = _allGames;

        if (!string.IsNullOrWhiteSpace(query))
        {
            filtered = _allGames.Where(g =>
                (g.Title?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (g.Platform?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (g.Genre?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false)
            );
        }

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Games.Clear();
            foreach (var g in filtered) Games.Add(g);
        });
    }

    
}