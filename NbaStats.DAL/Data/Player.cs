namespace NbaStats.DAL.Data;

public class Player
{
    public int Playerid { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Position { get; set; } = null!;

    public int? Teamid { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public virtual ICollection<PlayerSeasonAverage> Playerseasonaverages { get; set; } = new List<PlayerSeasonAverage>();

    public virtual ICollection<PlayerStat> Playerstats { get; set; } = new List<PlayerStat>();

    public virtual Team? Team { get; set; }

    public virtual ICollection<UserPreference> Userpreferences { get; set; } = new List<UserPreference>();
}
