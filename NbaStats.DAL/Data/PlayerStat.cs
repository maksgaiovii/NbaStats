using System.ComponentModel.DataAnnotations.Schema;

namespace NbaStats.DAL.Data;

public class PlayerStat
{
    public int PlayerStatsId { get; set; }

    public int PlayerId { get; set; }

    public int MatchId { get; set; }

    public int FgMade { get; set; }

    public int FgAttempted { get; set; }

    public int FreeThrowsMade { get; set; }

    public int FreeThrowsAttempted { get; set; }

    public int ThreePointersMade { get; set; }

    public int ThreePointersAttempted { get; set; }

    public int Points { get; set; }

    public int Assists { get; set; }

    public int Rebounds { get; set; }

    public int Steals { get; set; }
    
    [Column("turnovers")]
    public int Turnovers { get; set; }
    
    [Column("blocks")]
    public int Blocks { get; set; }

    public decimal MinutesPlayed { get; set; }

    public virtual required Match Match { get; set; }

    public virtual required Player Player { get; set; }
}
