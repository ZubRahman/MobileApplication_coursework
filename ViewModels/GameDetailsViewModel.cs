using MyMauiApp.Models;
using MyMauiApp.Services.Interfaces;
using System.Windows.Input;

namespace MyMauiApp.ViewModels;

public class GameDetailsViewModel : BaseViewModel
{
    private readonly IGameCatalogService _gameService;

    private Game? _game;
    public Game? Game
    {
        get => _game;
        set
        {
            if (_game == value) return;
            _game = value;

            OnPropertyChanged(nameof(Game));
            OnPropertyChanged(nameof(GameTitle));
            OnPropertyChanged(nameof(GamePlatform));
            OnPropertyChanged(nameof(GameGenre));
            OnPropertyChanged(nameof(GameSummary));
            OnPropertyChanged(nameof(GameCoverUrl));
        }
    }

    public string GameTitle => Game?.Title ?? string.Empty;
    public string GamePlatform => Game?.Platform ?? string.Empty;
    public string GameGenre => Game?.Genre ?? string.Empty;
    public string GameSummary => Game?.Summary ?? string.Empty;
    public string GameCoverUrl => Game?.CoverUrl ?? string.Empty;

    public GameDetailsViewModel(IGameCatalogService gameService)
    {
        _gameService = gameService;
        OpenRateCommand = new Command(async () => await OpenRate());
    }

    public async Task LoadByIdAsync(string id)
    {
        try
        {
            Game = await _gameService.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }

        
    }

    public ICommand OpenRateCommand { get; }
    private async Task OpenRate()
    {
        if (Game is null) return;

        await Shell.Current.GoToAsync(
            $"editrating?gameId={Uri.EscapeDataString(Game.Id)}&title={Uri.EscapeDataString(Game.Title)}"
        );
    }

}


