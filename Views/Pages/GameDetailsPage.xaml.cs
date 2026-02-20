using MyMauiApp.ViewModels;

namespace MyMauiApp.Views.Pages;

[QueryProperty(nameof(GameId), "id")]
public partial class GameDetailsPage : ContentPage
{
    private readonly GameDetailsViewModel _vm;

    private string _gameId = string.Empty;
    public string GameId
    {
        get => _gameId;
        set
        {
            _gameId = value;
            OnPropertyChanged();
            _ = _vm.LoadByIdAsync(_gameId);
        }
    }

    public GameDetailsPage(GameDetailsViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}