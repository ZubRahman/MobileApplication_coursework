using MyMauiApp.Models;
namespace MyMauiApp.Services.Interfaces;
using MyMauiApp.Data.Repositories;



public interface ISupabaseService
{
    Supabase.Client Client { get; }
    Task InitializeAsync();
    Task<DiaryEntry?> AddDiaryEntryAsync(DiaryEntry entry);
    Task<List<DiaryEntry>> GetCurrentUserDiaryEntriesAsync();

    Task SignUpAsync(string email, string password);
    Task SignInAsync(string email, string password);
    Task SignOutAsync();

    Task UpdateDiaryEntryAsync(DiaryEntry entry);
    Task DeleteDiaryEntryAsync(DiaryEntry entry);
    Task SyncUnsyncedLocalEntriesAsync(List<DiaryEntry> localEntries, IDiaryRepository repo);
    Task PullCloudEntriesToLocalAsync(IDiaryRepository repo);

    string? GetCurrentUserId();
    string? GetCurrentUserEmail();
}
