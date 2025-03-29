using System.Linq.Expressions;
using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface IPlayerStatRepository
{
    Task<IEnumerable<Player>> GetPlayersWithMostStatAsync(int topN,
        Expression<Func<PlayerStat, int>> statSelector);
}