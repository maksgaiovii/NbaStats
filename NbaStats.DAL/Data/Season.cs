using System;
using System.Collections.Generic;

namespace NbaStats.DAL.Data;

public partial class Season
{
    public int Seasonid { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public int Year { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<Playerseasonaverage> Playerseasonaverages { get; set; } = new List<Playerseasonaverage>();

    public virtual ICollection<Teamseasonaverage> Teamseasonaverages { get; set; } = new List<Teamseasonaverage>();
}
