using System.Linq.Expressions;
using NbaStats.DAL.Data;

namespace NbaStats.BLL.Interfaces;

public interface IPlayerSeasonAverageService : IService<PlayerSeasonAverage>
{
    Task<bool> UpdateStatAsync(int playerSeasonAverageId,
        Expression<Func<PlayerSeasonAverage, double>> statSelector, double newValue);
            
    Task<IEnumerable<PlayerSeasonAverage>> GetPlayerSeasonAveragesByPlayerAsync(int playerId);
        
    Task<IEnumerable<PlayerSeasonAverage>> GetPlayerSeasonAveragesBySeasonAsync(int seasonId, int playerId);
}