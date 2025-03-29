using System;
using System.Collections.Generic;

namespace NbaStats.DAL.Data;

public partial class Playerstat
{
    public int Playerstatsid { get; set; }

    public int? Playerid { get; set; }

    public int? Matchid { get; set; }

    public int? Fgmade { get; set; }

    public int? Fgattempted { get; set; }

    public int? Freethrowsmade { get; set; }

    public int? Freethrowsattempted { get; set; }

    public int? Threepointersmade { get; set; }

    public int? Threepointersattempted { get; set; }

    public int? Points { get; set; }

    public int? Assists { get; set; }

    public int? Rebounds { get; set; }

    public int? Steals { get; set; }

    public decimal? Minutesplayed { get; set; }

    public virtual Match? Match { get; set; }

    public virtual Player? Player { get; set; }
}
