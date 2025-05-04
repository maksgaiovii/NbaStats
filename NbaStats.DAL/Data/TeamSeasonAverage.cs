using System.ComponentModel.DataAnnotations.Schema;

namespace NbaStats.DAL.Data;

public class TeamSeasonAverage
{
    public int TeamSeasonAveragesId { get; set; }

    public int TeamId { get; set; }

    public int SeasonId { get; set; }

    public decimal AvgPoints { get; set; }

    public decimal AvgAssists { get; set; }

    public decimal AvgRebounds { get; set; }

    [Column("avgturnovers")]
    public decimal AvgTurnovers { get; set; }
    
    public decimal AvgSteals { get; set; }
    
    [Column("avgblocks")]
    public decimal AvgBlocks { get; set; }

    public virtual required Season Season { get; set; }

    public virtual  required Team Team { get; set; }
}
