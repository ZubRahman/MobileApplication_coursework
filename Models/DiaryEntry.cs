using SQLite;

namespace MyMauiApp.Models;
using Microsoft.Maui.Graphics;
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
    public Color ProgressColor => ProgressLevel switch
    {
        "Abandoned" => Colors.Red,
        "Started" => Colors.Orange,
        "In Progress" => Colors.Goldenrod,
        "Completed" => Colors.Green,
        "100% Achievements" => Colors.ForestGreen,
        "Completionist" => Colors.MediumSeaGreen,
        _ => Colors.Gray
    };

    public DateTime PlayedOn { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}