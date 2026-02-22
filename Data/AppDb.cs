using SQLite;
using MyMauiApp.Models;

namespace MyMauiApp.Data;

public class AppDb
{
    private SQLiteAsyncConnection? _db;

    private async Task<SQLiteAsyncConnection> GetDbAsync()
    {
        if (_db != null) return _db;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "mymauiapp.db3");
        _db = new SQLiteAsyncConnection(dbPath);

        await _db.CreateTableAsync<DiaryEntry>();
        return _db;
    }

    public async Task<SQLiteAsyncConnection> ConnectionAsync() => await GetDbAsync();
}