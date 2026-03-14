using MyMauiApp.ViewModels;

namespace MyMauiApp.Views.Pages;

public partial class DiaryPage : ContentPage
{
    public DiaryPage(DiaryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is DiaryViewModel vm)
            await vm.LoadEntriesAsync();
    }
}