using MyMauiApp.Models;
using MyMauiApp.Services.Interfaces;
using System.Linq;

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
    public async Task<DiaryEntry?> AddDiaryEntryAsync(DiaryEntry entry)
    {
        var currentUserId = GetCurrentUserId();

        var cloudEntry = new SupabaseDiaryEntry
        {
            UserId = currentUserId,
            GameId = entry.GameId,
            GameTitle = entry.GameTitle,
            Rating = entry.Rating,
            Review = entry.Review,
            PlayedOn = entry.PlayedOn,
            CreatedAt = entry.CreatedAt
        };

        var response = await Client.From<SupabaseDiaryEntry>().Insert(cloudEntry);

        var inserted = response.Models.FirstOrDefault();
        if (inserted is null) return null;

        return new DiaryEntry
        {
            CloudId = inserted.Id,
            UserId = inserted.UserId ?? string.Empty,
            GameId = inserted.GameId,
            GameTitle = inserted.GameTitle,
            Rating = inserted.Rating,
            Review = inserted.Review,
            PlayedOn = inserted.PlayedOn,
            CreatedAt = inserted.CreatedAt
        };
    }

    public async Task<List<DiaryEntry>> GetCurrentUserDiaryEntriesAsync()
    {
        var currentUserId = GetCurrentUserId();

        if (string.IsNullOrWhiteSpace(currentUserId))
            return new List<DiaryEntry>();

        var response = await Client
            .From<SupabaseDiaryEntry>()
            .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, currentUserId)
            .Get();

        var results = response.Models.Select(row => new DiaryEntry
        {
            GameId = row.GameId,
            GameTitle = row.GameTitle,
            Rating = row.Rating,
            Review = row.Review,
            PlayedOn = row.PlayedOn,
            CreatedAt = row.CreatedAt
        }).ToList();

        return results;
    }

    public async Task UpdateDiaryEntryAsync(DiaryEntry entry)
    {
        if (entry.CloudId is null)
            return;

        var currentUserId = GetCurrentUserId();

        var cloudEntry = new SupabaseDiaryEntry
        {
            Id = entry.CloudId.Value,
            UserId = string.IsNullOrWhiteSpace(entry.UserId) ? currentUserId : entry.UserId,
            GameId = entry.GameId,
            GameTitle = entry.GameTitle,
            Rating = entry.Rating,
            Review = entry.Review,
            PlayedOn = entry.PlayedOn,
            CreatedAt = entry.CreatedAt
        };

        await Client
            .From<SupabaseDiaryEntry>()
            .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, entry.CloudId.Value.ToString())
            .Update(cloudEntry);
    }

    public async Task DeleteDiaryEntryAsync(DiaryEntry entry)
    {
        if (entry.CloudId is null)
            return;

        await Client
            .From<SupabaseDiaryEntry>()
            .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, entry.CloudId.Value.ToString())
            .Delete();
    }

    public async Task SignUpAsync(string email, string password)
    {
        await Client.Auth.SignUp(email, password);
    }

    public async Task SignInAsync(string email, string password)
    {
        await Client.Auth.SignIn(email, password);
    }

    public async Task SignOutAsync()
    {
        await Client.Auth.SignOut();
    }

    public string? GetCurrentUserId()
    {
        return Client.Auth.CurrentUser?.Id;
    }
    public string? GetCurrentUserEmail()
    {
        return Client.Auth.CurrentUser?.Email;
    }

    
    
}