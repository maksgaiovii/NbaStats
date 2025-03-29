using System;
using System.Collections.Generic;

namespace NbaStats.DAL.Data;

public partial class User
{
    public int Userid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Userpreference> Userpreferences { get; set; } = new List<Userpreference>();
}
