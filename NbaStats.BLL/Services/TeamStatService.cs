using System.Linq.Expressions;
using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class TeamStatService : Service<TeamStat>, ITeamStatService
{
    private readonly ITeamStatRepository teamStatRepository;
    
    public TeamStatService(ITeamStatRepository repository) : base(repository)
    {
        teamStatRepository = repository;
    }
    
    
    public async Task<bool> UpdateStatAsync(int teamStatId, Expression<Func<TeamStat, double>> statSelector, double newValue)
    {
        return await teamStatRepository.UpdateStatAsync(teamStatId, statSelector, newValue);
    }
    
    public async Task<IEnumerable<TeamStat>> GetTeamStatsByGameAsync(int gameId)
    {
        return await teamStatRepository.GetTeamStatsByGameAsync(gameId);
    }
}