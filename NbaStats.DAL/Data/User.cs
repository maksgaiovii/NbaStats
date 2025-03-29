namespace NbaStats.DAL.Data;

public class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();
}
