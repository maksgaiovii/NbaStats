
namespace NbaStats.DAL.Data;

public class PlayerSeasonAverage
{
    public int PlayerSeasonAveragesId { get; set; }

    public int? PlayerId { get; set; }

    public int? SeasonId { get; set; }

    public decimal? AvgPoints { get; set; }

    public decimal? AvgAssists { get; set; }

    public decimal? AvgSteals { get; set; }

    public decimal? AvgRebounds { get; set; }

    public decimal? AvgTurnovers { get; set; }

    public decimal? AvgMinutesPlayed { get; set; }

    public virtual Player? Player { get; set; }

    public virtual Season? Season { get; set; }
}
