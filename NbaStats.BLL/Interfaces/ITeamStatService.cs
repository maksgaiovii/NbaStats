using System.Linq.Expressions;
using NbaStats.DAL.Data;

namespace NbaStats.BLL.Interfaces;

public interface ITeamStatService : IService<TeamStat>
{
    Task<bool> UpdateStatAsync(int teamStatId,
        Expression<Func<TeamStat, double>> statSelector, double newValue);
        
    Task<IEnumerable<TeamStat>> GetTeamStatsByGameAsync(int gameId);
}