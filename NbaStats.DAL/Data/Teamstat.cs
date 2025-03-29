using System;
using System.Collections.Generic;

namespace NbaStats.DAL.Data;

public partial class Teamstat
{
    public int Teamstatsid { get; set; }

    public int? Matchid { get; set; }

    public int? Teamid { get; set; }

    public int? Wins { get; set; }

    public int? Losses { get; set; }

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

    public virtual Match? Match { get; set; }

    public virtual Team? Team { get; set; }
}
