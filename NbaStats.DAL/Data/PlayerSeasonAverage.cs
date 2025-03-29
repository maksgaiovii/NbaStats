
namespace NbaStats.DAL.Data;

public class PlayerSeasonAverage
{
    public int Playerseasonaveragesid { get; set; }

    public int? Playerid { get; set; }

    public int? Seasonid { get; set; }

    public decimal? Avgpoints { get; set; }

    public decimal? Avgassists { get; set; }

    public decimal? Avgsteals { get; set; }

    public decimal? Avgrebounds { get; set; }

    public decimal? Avgturnovers { get; set; }

    public decimal? Avgminutesplayed { get; set; }

    public virtual Player? Player { get; set; }

    public virtual Season? Season { get; set; }
}
