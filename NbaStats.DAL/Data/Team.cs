using System;
using System.Collections.Generic;

namespace NbaStats.DAL.Data;

public partial class Team
{
    public int Teamid { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? Conference { get; set; }

    public string? Division { get; set; }

    public virtual ICollection<Match> MatchAwayteams { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchHometeams { get; set; } = new List<Match>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<Teamseasonaverage> Teamseasonaverages { get; set; } = new List<Teamseasonaverage>();

    public virtual ICollection<Teamstat> Teamstats { get; set; } = new List<Teamstat>();

    public virtual ICollection<Userpreference> Userpreferences { get; set; } = new List<Userpreference>();
}
