using MyMauiApp.ViewModels;

namespace MyMauiApp.Views.Pages;

public partial class SignUpPage : ContentPage
{
    public SignUpPage(AuthViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}