using System.Linq.Expressions;
using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class TeamService : Service<Team>, ITeamService
{
    private readonly ITeamRepository teamRepository;
    
    public TeamService(ITeamRepository teamRepository) : base(teamRepository)
    {
        this.teamRepository = teamRepository;
    }


    public async Task<IEnumerable<Team>> GetTeamsByDivisionAsync(string divisionName)
    {
        return await teamRepository.GetTeamsByDivisionAsync(divisionName);
    }
    
    public async Task<IEnumerable<Team>> GetTeamsByConferenceAsync(string conferenceName)
    {
        return await teamRepository.GetTeamsByConferenceAsync(conferenceName);
    }
    
    public async Task<IEnumerable<Team>> GetTeamWithMostStatAsync(int topN, Expression<Func<TeamStat, int>> statSelector)
    {
        return await teamRepository.GetTeamWithMostStatAsync(topN, statSelector);
    }
    
    public async Task<IEnumerable<Team>> GetTeamsWithMostStatAverageAsync(int topN, Expression<Func<TeamSeasonAverage, double>> statSelector)
    {
        return await teamRepository.GetTeamsWithMostStatAverageAsync(topN, statSelector);
    }
}