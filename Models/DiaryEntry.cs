using SQLite;

namespace MyMauiApp.Models;

public class DiaryEntry
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string GameId { get; set; } = string.Empty;
    public string GameTitle { get; set; } = string.Empty;

    public int Rating { get; set; }
    public string Review { get; set; } = string.Empty;

    public DateTime PlayedOn { get; set; }
    public DateTime CreatedAt { get; set; }
}