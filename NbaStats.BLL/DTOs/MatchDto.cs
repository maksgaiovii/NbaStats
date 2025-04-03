namespace NbaStats.BLL.DTOs;

public class MatchDto
{
    public int Id { get; set; }
    
    public DateOnly Date { get; set; }

    public string HomeTeam { get; set; }

    public string AwayTeam { get; set; }

    public int HomeScore { get; set; }

    public int AwayScore { get; set; }
}