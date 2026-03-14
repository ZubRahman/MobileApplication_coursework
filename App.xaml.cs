using MyMauiApp.Services.Interfaces;

namespace MyMauiApp;

public partial class App : Application
{
    public App(ISupabaseService supabaseService)
    {
        InitializeComponent();
        MainPage = new AppShell();

        _ = InitializeSupabaseAsync(supabaseService);
    }

    private async Task InitializeSupabaseAsync(ISupabaseService supabaseService)
    {
        try
        {
            await supabaseService.InitializeAsync();
            System.Diagnostics.Debug.WriteLine("Supabase initialized successfully.");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Supabase init failed: {ex}");
        }
    }
}