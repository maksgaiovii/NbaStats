using System.Linq.Expressions;
using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface ITeamStatRepository
{
    Task<IEnumerable<Team>> GetTeamWithMostStatAsync(int topN,
        Expression<Func<TeamStat, int>> statSelector);
}