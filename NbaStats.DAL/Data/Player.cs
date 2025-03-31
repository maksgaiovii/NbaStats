namespace NbaStats.DAL.Data;

public class Player
{
    public int PlayerId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Position { get; set; } = null!;

    public int TeamId { get; set; }

    public decimal Height { get; set; }

    public decimal Weight { get; set; }

    public DateTime BirthDate { get; set; }
    
    public virtual ICollection<PlayerSeasonAverage> PlayerSeasonAverages { get; set; } = new List<PlayerSeasonAverage>();

    public virtual ICollection<PlayerStat> PlayerStats { get; set; } = new List<PlayerStat>();

    public virtual required Team Team { get; set; }

    public virtual ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();
}
