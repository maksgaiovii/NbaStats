namespace NbaStats.DAL.Data;

public class Match
{
    public int MatchId { get; set; }

    public DateOnly Date { get; set; }

    public int? HomeTeamId { get; set; }

    public int? AwayTeamId { get; set; }

    public int? HomeScore { get; set; }

    public int? AwayScore { get; set; }

    public int? SeasonId { get; set; }

    public virtual Team? AwayTeam { get; set; }

    public virtual Team? HomeTeam { get; set; }

    public virtual ICollection<PlayerStat> PlayerStats { get; set; } = new List<PlayerStat>();

    public virtual Season? Season { get; set; }

    public virtual ICollection<TeamStat> TeamStats { get; set; } = new List<TeamStat>();
}
