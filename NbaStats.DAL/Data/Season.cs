
namespace NbaStats.DAL.Data;

public class Season
{
    public int Seasonid { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public int Year { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<PlayerSeasonAverage> Playerseasonaverages { get; set; } = new List<PlayerSeasonAverage>();

    public virtual ICollection<TeamSeasonAverage> Teamseasonaverages { get; set; } = new List<TeamSeasonAverage>();
}
