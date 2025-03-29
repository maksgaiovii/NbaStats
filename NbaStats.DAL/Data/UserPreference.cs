namespace NbaStats.DAL.Data;

public class UserPreference
{
    public int UserPreferencesId { get; set; }

    public int? Userid { get; set; }

    public int? TeamId { get; set; }

    public int? PlayerId { get; set; }

    public virtual Player? Player { get; set; }

    public virtual Team? Team { get; set; }

    public virtual User? User { get; set; }
}
