using NbaStats.BLL.DTOs;
using NbaStats.BLL.Interfaces;
using NbaStats.BLL.Mappers;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class PlayerService : Service<Player>, IPlayerService
{
    private readonly IPlayerRepository playerRepository;
    private readonly ITeamRepository teamRepository;

    public PlayerService(IPlayerRepository playerRepository, ITeamRepository teamRepository) : base(playerRepository)
    {
        this.playerRepository = playerRepository;
        this.teamRepository = teamRepository;
    }


    public async Task<PlayerDto> CreatePlayerAsync(PlayerCreateDto playerDto)
    {
        var team = await teamRepository.GetTeamByNameAsync(playerDto.TeamName);
        if (team == null)
        {
            throw new ArgumentException($"Team with name {playerDto.TeamName} not found");
        }

        var playerEntity = PlayerCreateDtoMapper.ToEntity(playerDto, team);
        await playerRepository.AddAsync(playerEntity);
        return PlayerCreateDtoMapper.ToDto(playerEntity);
    }

    public async Task<PlayerDto> UpdateAsync(int id, PlayerCreateDto playerDto)
    {
        var existingPlayer = await playerRepository.GetByIdAsync(id);
        if (existingPlayer == null)
        {
            throw new ArgumentException($"Player with ID {id} not found");
        }

        var team = await teamRepository.GetTeamByNameAsync(playerDto.TeamName);
        if (team == null)
        {
            throw new ArgumentException($"Team with name {playerDto.TeamName} not found");
        }

        existingPlayer.Name = playerDto.Name;
        existingPlayer.Surname = playerDto.Surname;
        existingPlayer.Position = playerDto.Position;
        existingPlayer.TeamId = team.TeamId;
        existingPlayer.Team = team;
        existingPlayer.Height = playerDto.Height;
        existingPlayer.Weight = playerDto.Weight;
        existingPlayer.BirthDate = playerDto.BirthDate;

        var isUpdated = await playerRepository.UpdateAsync(existingPlayer);

        return isUpdated
            ? PlayerCreateDtoMapper.ToDto(existingPlayer)
            : throw new InvalidOperationException("Failed to update player");
    }
    
    public async Task<bool> DeleteAsync(PlayerDto playerDto)
    {
        var existingPlayer = await playerRepository.GetByIdAsync(playerDto.PlayerId);
        if (existingPlayer == null)
        {
            throw new ArgumentException($"Player with ID {playerDto.PlayerId} not found");
        }

        return await playerRepository.DeleteAsync(existingPlayer);
    }
}