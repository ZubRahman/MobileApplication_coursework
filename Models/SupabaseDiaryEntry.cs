using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MyMauiApp.Models;

[Table("diary_entries")]
public class SupabaseDiaryEntry : BaseModel
{
    [PrimaryKey("id", false)]
    public long Id { get; set; }

    [Column("user_id")]
    public string? UserId { get; set; }

    [Column("game_id")]
    public string GameId { get; set; } = string.Empty;

    [Column("game_title")]
    public string GameTitle { get; set; } = string.Empty;

    [Column("rating")]
    public int Rating { get; set; }

    [Column("review")]
    public string Review { get; set; } = string.Empty;

    [Column("played_on")]
    public DateTime PlayedOn { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}