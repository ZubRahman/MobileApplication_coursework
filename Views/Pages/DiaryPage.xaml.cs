using MyMauiApp.ViewModels;

namespace MyMauiApp.Views.Pages;

public partial class DiaryPage : ContentPage
{
    private readonly DiaryViewModel _vm;

    public DiaryPage(DiaryViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadEntriesAsync();
    }
}