using MyMauiApp.Models;
using MyMauiApp.ViewModels;

namespace MyMauiApp.Views.Pages;

public partial class GamesPage : ContentPage
{
    private readonly GamesViewModel _vm;

    public GamesPage(GamesViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    private async void OnGameTapped(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is not Game game)
            return;

        await Shell.Current.GoToAsync(
            $"gamedetails?gameId={Uri.EscapeDataString(game.Id)}");
    }
}