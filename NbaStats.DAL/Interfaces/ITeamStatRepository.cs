using System.Linq.Expressions;
using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface ITeamStatRepository : IRepository<TeamStat>
{
    Task<bool> UpdateStatAsync(int teamStatId,
            Expression<Func<TeamStat, double>> statSelector, double newValue);
        
        Task<IEnumerable<TeamStat>> GetTeamStatsByGameAsync(int gameId);
}