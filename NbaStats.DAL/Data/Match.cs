using System;
using System.Collections.Generic;

namespace NbaStats.DAL.Data;

public partial class Match
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

    public virtual ICollection<Playerstat> Playerstats { get; set; } = new List<Playerstat>();

    public virtual Season? Season { get; set; }

    public virtual ICollection<Teamstat> Teamstats { get; set; } = new List<Teamstat>();
}
