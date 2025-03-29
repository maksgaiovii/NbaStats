namespace NbaStats.DAL.Data;

public class User
{
    public int Userid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<UserPreference> Userpreferences { get; set; } = new List<UserPreference>();
}
