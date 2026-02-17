namespace MyMauiApp.Views.Pages;

[QueryProperty(nameof(GameId), "id")]
public partial class GameDetailsPage : ContentPage
{
    private string _gameId = string.Empty;

    public string GameId
    {
        get => _gameId;
        set
        {
            _gameId = value;
            OnPropertyChanged();
        }
    }

    public GameDetailsPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
