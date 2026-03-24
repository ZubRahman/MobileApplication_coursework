using MyMauiApp.Views.Pages;

namespace MyMauiApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("login", typeof(LoginPage));
        Routing.RegisterRoute("gamedetails", typeof(GameDetailsPage));
        Routing.RegisterRoute("editrating", typeof(EditRatingPage));
        Routing.RegisterRoute("diaryentrydetails", typeof(DiaryEntryDetailsPage));
    }
}
