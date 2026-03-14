using MyMauiApp.Models;
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
            SupabaseConfig.PublishableKey,
            options
        );

        await Client.InitializeAsync();
    }
    public async Task AddDiaryEntryAsync(DiaryEntry entry)
    {
        var cloudEntry = new SupabaseDiaryEntry
        {
            GameId = entry.GameId,
            GameTitle = entry.GameTitle,
            Rating = entry.Rating,
            Review = entry.Review,
            PlayedOn = entry.PlayedOn,
            CreatedAt = entry.CreatedAt
        };

        await Client.From<SupabaseDiaryEntry>().Insert(cloudEntry);
    }
    
}