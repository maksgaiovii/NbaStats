using NbaStats.BLL.DTOs;
using NbaStats.DAL.Data;

namespace NbaStats.Mappers;

public static class PlayerDtoMapper
{
    public static PlayerDto ToDto(this Player player)
        {
            return new PlayerDto
            {
                FullName = player.Name + " " + player.Surname,
                Position = player.Position,
                TeamName = player.Team.Name ?? string.Empty,
                Height = player.Height,
                Weight = player.Weight,
                Age = DateTime.Now.Year - player.BirthDate.Year
            };
        }
    
        public static Player ToEntity(this PlayerDto dto, Team team, DateTime birthDate)
        {
            List<string> nameParts = dto.FullName.Split(' ').ToList();
            return new Player
            {
                Name = nameParts[0],
                Surname = nameParts[1],
                Position = dto.Position,
                TeamId = team.TeamId,
                Height = dto.Height,
                Weight = dto.Weight,
                BirthDate = birthDate,
                Team = team
            };
        }
    
        
}