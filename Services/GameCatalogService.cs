using System.Text.Json;
using MyMauiApp.Models;
using MyMauiApp.Services.Interfaces;
using System.Linq;

namespace MyMauiApp.Services;

public class GameCatalogService : IGameCatalogService
{
    public async Task<IReadOnlyList<Game>> GetGamesAsync()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("games_seed.json");
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();

        var games = JsonSerializer.Deserialize<List<Game>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Console.WriteLine($"Games loaded: {games?.Count ?? 0}");
        return games ?? new List<Game>();
    }
    public async Task<Game?> GetByIdAsync(string id)
    {
    if (string.IsNullOrWhiteSpace(id)) return null;
        var games = await GetGamesAsync();
        return games.FirstOrDefault(g => string.Equals(g.Id, id, StringComparison.OrdinalIgnoreCase));
    }
}
