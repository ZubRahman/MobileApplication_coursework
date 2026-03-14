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
            Console.WriteLine("Supabase initialized successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Supabase init failed: {ex}");
        }
    }
}