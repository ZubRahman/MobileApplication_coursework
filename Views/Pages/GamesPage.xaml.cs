using MyMauiApp.ViewModels;

namespace MyMauiApp.Views.Pages;

public partial class GamesPage : ContentPage
{
    public GamesPage()
    {
        InitializeComponent();

        var services = Application.Current?.Handler?.MauiContext?.Services;
        BindingContext = services?.GetService(typeof(GamesViewModel)) as GamesViewModel;
    }

    // Keep this too if you want (optional)
    public GamesPage(GamesViewModel vm) : this()
    {
        BindingContext = vm;
    }
}
