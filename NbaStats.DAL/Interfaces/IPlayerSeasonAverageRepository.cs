using System.Linq.Expressions;
using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface IPlayerSeasonAverageRepository : IRepository<PlayerSeasonAverage>
{
    Task<bool> UpdateStatAsync(int playerSeasonAverageId,
            Expression<Func<PlayerSeasonAverage, double>> statSelector, double newValue);
            
        Task<IEnumerable<PlayerSeasonAverage>> GetPlayerSeasonAveragesByPlayerAsync(int playerId);
        
        Task<IEnumerable<PlayerSeasonAverage>> GetPlayerSeasonAveragesBySeasonAsync(int seasonId, int playerId);
        
}