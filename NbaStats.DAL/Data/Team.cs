namespace NbaStats.DAL.Data;

public class Team
{
    public int Teamid { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? Conference { get; set; }

    public string? Division { get; set; }

    public virtual ICollection<Match> MatchAwayteams { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchHometeams { get; set; } = new List<Match>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<TeamSeasonAverage> Teamseasonaverages { get; set; } = new List<TeamSeasonAverage>();

    public virtual ICollection<TeamStat> Teamstats { get; set; } = new List<TeamStat>();

    public virtual ICollection<UserPreference> Userpreferences { get; set; } = new List<UserPreference>();
}
