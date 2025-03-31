using System.Linq.Expressions;
using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;
public interface ITeamRepository : IRepository<Team>
{
    Task<IEnumerable<Team>> GetTeamsByDivisionAsync(string divisionName);
    
    Task<IEnumerable<Team>> GetTeamsByConferenceAsync(string conferenceName);
    
    Task<IEnumerable<Team>> GetTeamWithMostStatAsync(int topN,
        Expression<Func<TeamStat, int>> statSelector);
    
    Task<IEnumerable<Team>> GetTeamsWithMostStatAverageAsync(int topN, 
        Expression<Func<TeamSeasonAverage, double>> statSelector);
}