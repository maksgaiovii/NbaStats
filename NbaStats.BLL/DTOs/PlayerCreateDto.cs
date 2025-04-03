using System.ComponentModel.DataAnnotations;

namespace NbaStats.BLL.DTOs;

public class PlayerCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Surname { get; set; }
    
    [Required]
    public string Position { get; set; }
    
    [Required]
    public string TeamName { get; set; }
    
    [Range(1, 3)]
    public decimal Height { get; set; }
    
    [Range(50, 200)]
    public decimal Weight { get; set; }
    
    public DateTime BirthDate { get; set; }
}