
namespace NbaStats.DAL.Data;

public class Season
{
    public int SeasonId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int Year { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<PlayerSeasonAverage> PlayerSeasonAverages { get; set; } = new List<PlayerSeasonAverage>();

    public virtual ICollection<TeamSeasonAverage> TeamSeasonAverages { get; set; } = new List<TeamSeasonAverage>();
}
