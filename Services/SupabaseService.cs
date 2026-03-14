using MyMauiApp.Services.Interfaces;

namespace MyMauiApp.Services;

public class SupabaseService : ISupabaseService
{
    public Supabase.Client Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = false
        };

        Client = new Supabase.Client(
            SupabaseConfig.Url,
            SupabaseConfig.AnonKey,
            options
        );

        await Client.InitializeAsync();
    }
}