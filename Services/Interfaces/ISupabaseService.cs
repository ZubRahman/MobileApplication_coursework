using MyMauiApp.Models;

namespace MyMauiApp.Services.Interfaces;

public interface ISupabaseService
{
    Supabase.Client Client { get; }
    Task InitializeAsync();
    Task<DiaryEntry?> AddDiaryEntryAsync(DiaryEntry entry);
    Task<List<DiaryEntry>> GetCurrentUserDiaryEntriesAsync();

    Task SignUpAsync(string email, string password);
    Task SignInAsync(string email, string password);
    Task SignOutAsync();
    string? GetCurrentUserId();
    string? GetCurrentUserEmail();
}
