using SQLite;

namespace MyMauiApp.Models;

public class DiaryEntry
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public long? CloudId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public bool NeedsSync { get; set; }

    [Indexed]
    public string GameId { get; set; } = string.Empty;
    public string GameTitle { get; set; } = string.Empty;

    public int Rating { get; set; }
    public string Review { get; set; } = string.Empty;
    public string ProgressLevel { get; set; } = "Played a Bit";

    public DateTime PlayedOn { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}