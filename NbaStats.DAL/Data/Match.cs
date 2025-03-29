namespace NbaStats.DAL.Data;

public class Match
{
    public int Matchid { get; set; }

    public DateOnly Date { get; set; }

    public int? Hometeamid { get; set; }

    public int? Awayteamid { get; set; }

    public int? Homescore { get; set; }

    public int? Awayscore { get; set; }

    public int? Seasonid { get; set; }

    public virtual Team? Awayteam { get; set; }

    public virtual Team? Hometeam { get; set; }

    public virtual ICollection<PlayerStat> Playerstats { get; set; } = new List<PlayerStat>();

    public virtual Season? Season { get; set; }

    public virtual ICollection<TeamStat> Teamstats { get; set; } = new List<TeamStat>();
}
