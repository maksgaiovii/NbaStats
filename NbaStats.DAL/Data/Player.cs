using System;
using System.Collections.Generic;

namespace NbaStats.DAL.Data;

public partial class Player
{
    public int Playerid { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Position { get; set; } = null!;

    public int? Teamid { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public virtual ICollection<Playerseasonaverage> Playerseasonaverages { get; set; } = new List<Playerseasonaverage>();

    public virtual ICollection<Playerstat> Playerstats { get; set; } = new List<Playerstat>();

    public virtual Team? Team { get; set; }

    public virtual ICollection<Userpreference> Userpreferences { get; set; } = new List<Userpreference>();
}
