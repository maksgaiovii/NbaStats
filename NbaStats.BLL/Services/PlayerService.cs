using NbaStats.BLL.DTOs;
using NbaStats.BLL.Mappers;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class PlayerService
{
    private readonly IPlayerRepository playerRepository;
    private readonly ITeamRepository teamRepository;

    public PlayerService(IPlayerRepository playerRepository, ITeamRepository teamRepository)
    {
        this.playerRepository = playerRepository;
        this.teamRepository = teamRepository;
    }

    public async Task<PlayerDto> GetPlayerByIdAsync(int id)
    {
        var player = await playerRepository.GetByIdAsync(id);

        return (player != null 
            ? PlayerCreateDtoMapper.ToDto(player) : null) ?? throw new InvalidOperationException();
    }

    public async Task<List<PlayerDto>> GetAllPlayersAsync()
    {
        var players = await playerRepository.GetAllAsync();
        return players.Select(PlayerCreateDtoMapper.ToDto).ToList();
    }


    public async Task<PlayerDto> CreatePlayerAsync(PlayerCreateDto playerDto)
    {
        var team = await teamRepository.GetByIdAsync(playerDto.TeamId);
        if (team == null)
        {
            throw new ArgumentException($"Team with ID {playerDto.TeamId} not found");
        }

        var playerEntity = PlayerCreateDtoMapper.ToEntity(playerDto, team);
        await playerRepository.AddAsync(playerEntity);
        return PlayerCreateDtoMapper.ToDto(playerEntity);
    }

    public async Task<PlayerDto> UpdatePlayerAsync(int id, PlayerCreateDto playerDto)
    {
        var existingPlayer = await playerRepository.GetByIdAsync(id);
        if (existingPlayer == null)
        {
            throw new ArgumentException($"Player with ID {id} not found");
        }

        var team = await teamRepository.GetByIdAsync(playerDto.TeamId);
        if (team == null)
        {
            throw new ArgumentException($"Team with ID {playerDto.TeamId} not found");
        }

        existingPlayer.Name = playerDto.Name;
        existingPlayer.Surname = playerDto.Surname;
        existingPlayer.Position = playerDto.Position;
        existingPlayer.TeamId = playerDto.TeamId;
        existingPlayer.Team = team;
        existingPlayer.Height = playerDto.Height;
        existingPlayer.Weight = playerDto.Weight;
        existingPlayer.BirthDate = playerDto.BirthDate;

        var isUpdated = await playerRepository.UpdateAsync(existingPlayer);
        
        return isUpdated ? PlayerCreateDtoMapper.ToDto(existingPlayer) : throw new InvalidOperationException("Failed to update player");
    }

    public async Task<bool> DeletePlayerAsync(int id)
    {
        return await playerRepository.DeleteAsync(id);
    }
}