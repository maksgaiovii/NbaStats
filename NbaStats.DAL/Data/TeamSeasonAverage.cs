namespace NbaStats.DAL.Data;

public class TeamSeasonAverage
{
    public int Teamseasonaveragesid { get; set; }

    public int? Teamid { get; set; }

    public int? Seasonid { get; set; }

    public decimal? Avgpoints { get; set; }

    public decimal? Avgassists { get; set; }

    public decimal? Avgrebounds { get; set; }

    public decimal? Avgturnovers { get; set; }

    public virtual Season? Season { get; set; }

    public virtual Team? Team { get; set; }
}
