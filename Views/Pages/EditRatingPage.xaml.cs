using MyMauiApp.ViewModels;

namespace MyMauiApp.Views.Pages;

[QueryProperty(nameof(GameId), "gameId")]
[QueryProperty(nameof(GameTitle), "title")]
public partial class EditRatingPage : ContentPage
{
    private readonly EditRatingViewModel _vm;

    private string _gameId = string.Empty;
    public string GameId
    {
        get => _gameId;
        set
        {
            _gameId = value;
            _vm.GameId = value;
        }
    }

    private string _gameTitle = string.Empty;
    public string GameTitle
    {
        get => _gameTitle;
        set
        {
            _gameTitle = value;
            _vm.GameTitle = value;
        }
    }

    public EditRatingPage(EditRatingViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}