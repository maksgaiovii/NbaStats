
using System.ComponentModel.DataAnnotations.Schema;

namespace NbaStats.DAL.Data;

public class PlayerSeasonAverage
{
    public int PlayerSeasonAveragesId { get; set; }

    public int PlayerId { get; set; }

    public int SeasonId { get; set; }

    public decimal AvgPoints { get; set; }

    public decimal AvgAssists { get; set; }

    public decimal AvgSteals { get; set; }

    public decimal AvgRebounds { get; set; }
    
    [Column("avgturnovers")]
    public decimal AvgTurnovers { get; set; }

    public decimal AvgMinutesPlayed { get; set; }
    
    [Column("avgblocks")]
    public decimal AvgBlocks { get; set; }

    public virtual required Player Player { get; set; }

    public virtual required Season Season { get; set; }
}
