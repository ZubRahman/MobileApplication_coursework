using MyMauiApp.Models;

namespace MyMauiApp.Services.Interfaces;

public interface ISupabaseService
{
    Supabase.Client Client { get; }
    Task InitializeAsync();
    Task AddDiaryEntryAsync(DiaryEntry entry);
}