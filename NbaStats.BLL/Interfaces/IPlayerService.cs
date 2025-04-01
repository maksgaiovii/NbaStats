using NbaStats.BLL.DTOs;
using NbaStats.DAL.Data;

namespace NbaStats.BLL.Interfaces;

public interface IPlayerService : IService<Player>
{
     Task<PlayerDto> CreatePlayerAsync(PlayerCreateDto playerDto);
     Task<PlayerDto> UpdateAsync(int id, PlayerCreateDto playerDto);
}