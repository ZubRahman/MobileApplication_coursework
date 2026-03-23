using MyMauiApp.Models;

namespace MyMauiApp.Data.Repositories;

public interface IDiaryRepository
{
    Task AddEntryAsync(DiaryEntry entry);
    Task<List<DiaryEntry>> GetEntriesAsync();

    Task<DiaryEntry?> GetByIdAsync(int id);
    Task UpdateEntryAsync(DiaryEntry entry);
    Task DeleteEntryAsync(int id);
    Task<List<DiaryEntry>> GetUnsyncedEntriesAsync();
}