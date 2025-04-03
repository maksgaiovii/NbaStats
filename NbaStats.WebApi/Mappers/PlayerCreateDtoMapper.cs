using NbaStats.BLL.DTOs;
using NbaStats.BLL.Helpers;
using NbaStats.DAL.Data;

namespace NbaStats.BLL.Mappers;

public static class PlayerCreateDtoMapper
{
    public static Player ToEntity(PlayerCreateDto dto, Team team)
    {
        
        return new Player
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Position = dto.Position,
            TeamId = team.TeamId,
            Height = dto.Height,
            Weight = dto.Weight,
            BirthDate = dto.BirthDate,
            Team = team
        };
    }
    
    public static PlayerDto ToDto(Player player)
    {
        return new PlayerDto
        {
            PlayerId = player.PlayerId,
            FullName = $"{player.Name} {player.Surname}",
            Position = player.Position,
            TeamName = player.Team.Name,
            Height = player.Height,
            Weight = player.Weight,
            Age = AgeHelper.CalculateAge(player.BirthDate)
        };
    }
    
}