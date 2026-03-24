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

    public async Task<DiaryEntry?> GetByIdAsync(int id)
    {
        var conn = await _db.ConnectionAsync();
        return await conn.Table<DiaryEntry>()
                        .Where(e => e.Id == id)
                        .FirstOrDefaultAsync();
    }

    public async Task UpdateEntryAsync(DiaryEntry entry)
    {
        var conn = await _db.ConnectionAsync();
        await conn.UpdateAsync(entry);
    }

    public async Task DeleteEntryAsync(int id)
    {
        var conn = await _db.ConnectionAsync();
        await conn.DeleteAsync<DiaryEntry>(id);
    }
   public async Task<List<DiaryEntry>> GetUnsyncedEntriesAsync()
    {
        var conn = await _db.ConnectionAsync();
        return await conn.Table<DiaryEntry>()
                        .Where(e => e.CloudId == null)
                        .ToListAsync();
    }

    public async Task<DiaryEntry?> GetByCloudIdAsync(long cloudId)
    {
        var conn = await _db.ConnectionAsync();
        return await conn.Table<DiaryEntry>()
                        .Where(e => e.CloudId == cloudId)
                        .FirstOrDefaultAsync();
    }

    public async Task<List<DiaryEntry>> GetEntriesNeedingSyncAsync()
    {
        var conn = await _db.ConnectionAsync();
        return await conn.Table<DiaryEntry>()
                        .Where(e => e.NeedsSync)
                        .ToListAsync();
    }

}