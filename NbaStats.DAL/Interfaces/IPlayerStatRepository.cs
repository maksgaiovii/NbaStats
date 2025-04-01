using System.Linq.Expressions;
using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface IPlayerStatRepository : IRepository<PlayerStat>
{
    Task<bool> UpdateStatAsync(int playerStatId,
        Expression<Func<PlayerStat, double>> statSelector, double newValue);


    Task<IEnumerable<PlayerStat>> GetPlayerStatsByGameAsync(int gameId);
}