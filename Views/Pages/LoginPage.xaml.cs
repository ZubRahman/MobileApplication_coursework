using MyMauiApp.ViewModels;

namespace MyMauiApp.Views.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(AuthViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}