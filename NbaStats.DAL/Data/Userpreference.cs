﻿using System;
using System.Collections.Generic;

namespace NbaStats.DAL.Data;

public partial class Userpreference
{
    public int Userpreferencesid { get; set; }

    public int? Userid { get; set; }

    public int? Teamid { get; set; }

    public int? Playerid { get; set; }

    public virtual Player? Player { get; set; }

    public virtual Team? Team { get; set; }

    public virtual User? User { get; set; }
}
