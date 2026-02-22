using MyMauiApp.Models;

namespace MyMauiApp.Data.Repositories;

public class DiaryRepository : IDiaryRepository
{
    private readonly Data.AppDb _db;

    public DiaryRepository(Data.AppDb db)
    {
        _db = db;
    }

    public async Task AddEntryAsync(DiaryEntry entry)
    {
        var conn = await _db.ConnectionAsync();
        await conn.InsertAsync(entry);
    }

    public async Task<List<DiaryEntry>> GetEntriesAsync()
    {
        var conn = await _db.ConnectionAsync();
        return await conn.Table<DiaryEntry>()
                         .OrderByDescending(e => e.CreatedAt)
                         .ToListAsync();
    }
}