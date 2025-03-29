namespace NbaStats.DAL.Data;

public class Team
{
    public int TeamId { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? Conference { get; set; }

    public string? Division { get; set; }

    public virtual ICollection<Match> MatchAwayTeams { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchHomeTeams { get; set; } = new List<Match>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<TeamSeasonAverage> TeamSeasonAverages { get; set; } = new List<TeamSeasonAverage>();

    public virtual ICollection<TeamStat> TeamStats { get; set; } = new List<TeamStat>();

    public virtual ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();
}
