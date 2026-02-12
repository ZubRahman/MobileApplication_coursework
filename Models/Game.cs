namespace MyMauiApp.Models;

public class Game
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string Summary { get; set; } = string.Empty;
    public string CoverUrl { get; set; } = string.Empty;
}