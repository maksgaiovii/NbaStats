namespace NbaStats.DAL.Data;

public class UserPreference
{
    public int UserPreferencesId { get; set; }

    public int Userid { get; set; }

    public int TeamId { get; set; }

    public int PlayerId { get; set; }

    public virtual required Player Player { get; set; }

    public virtual required Team Team { get; set; }

    public virtual required User User { get; set; }
}
