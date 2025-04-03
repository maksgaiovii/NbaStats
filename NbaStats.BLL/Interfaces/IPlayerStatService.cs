using NbaStats.DAL.Data;

namespace NbaStats.BLL.Interfaces;

public interface IPlayerStatService : IService<PlayerStat>
{
    Task<IEnumerable<PlayerStat>> GetPlayerStatsByGameAsync(int gameId);

    Task<IEnumerable<PlayerStat>> GetPlayerStatsByPlayerAsync(int playerId);
}