using MyMauiApp.Models;

namespace MyMauiApp.Services.Interfaces;

public interface IGameCatalogService
{
    Task<IReadOnlyList<Game>> GetGamesAsync();
    Task<Game?> GetByIdAsync(string id);

}