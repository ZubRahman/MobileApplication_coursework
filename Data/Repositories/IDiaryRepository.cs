using MyMauiApp.Models;

namespace MyMauiApp.Data.Repositories;

public interface IDiaryRepository
{
    Task AddEntryAsync(DiaryEntry entry);
    Task<List<DiaryEntry>> GetEntriesAsync();
}