namespace NbaStats.BLL.DTOs;

public class PlayerDto
{
    public int PlayerId { get; set; }
    public required string FullName { get; set; } 
    public required string Position { get; set; }
    public required string TeamName { get; set; } 
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int Age { get; set; }
}
