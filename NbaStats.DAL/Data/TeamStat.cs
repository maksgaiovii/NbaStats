﻿namespace NbaStats.DAL.Data;

public class TeamStat
{
    public int TeamStatsId { get; set; }

    public int? MatchId { get; set; }

    public int? TeamId { get; set; }

    public int? Wins { get; set; }

    public int? Losses { get; set; }

    public int? FgMade { get; set; }

    public int? FgAttempted { get; set; }

    public int? FreeThrowsMade { get; set; }

    public int? FreeThrowsAttempted { get; set; }

    public int? ThreePointersMade { get; set; }

    public int? ThreePointersAttempted { get; set; }

    public int? Points { get; set; }

    public int? Assists { get; set; }

    public int? Rebounds { get; set; }

    public int? Steals { get; set; }

    public virtual Match? Match { get; set; }

    public virtual Team? Team { get; set; }
}
