using System.Linq.Expressions;
using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface ITeamSeasonAverageRepository: IRepository<TeamSeasonAverage>
{
    Task<bool> UpdateStatAsync(int teamSeasonAverageId,
                Expression<Func<TeamSeasonAverage, double>> statSelector, double newValue);
                
        Task<IEnumerable<TeamSeasonAverage>> GetTeamSeasonAveragesByTeamAsync(int teamId);
        
        Task<IEnumerable<TeamSeasonAverage>> GetTeamSeasonAveragesBySeasonAsync(int seasonId);
}