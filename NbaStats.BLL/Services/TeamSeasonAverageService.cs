using System.Linq.Expressions;
using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class TeamSeasonAverageService : Service<TeamSeasonAverage>, ITeamSeasonAverageService
{
    private readonly ITeamSeasonAverageRepository teamSeasonAverageRepository;
    
    public TeamSeasonAverageService(ITeamSeasonAverageRepository repository) : base(repository)
    {
        teamSeasonAverageRepository = repository;
    }
    
    public async Task<bool> UpdateStatAsync(int teamSeasonAverageId, Expression<Func<TeamSeasonAverage, double>> statSelector, double newValue)
    {
        return await teamSeasonAverageRepository.UpdateStatAsync(teamSeasonAverageId, statSelector, newValue);
    }
    
    public async Task<IEnumerable<TeamSeasonAverage>> GetTeamSeasonAveragesByTeamAsync(int teamId)
    {
        return await teamSeasonAverageRepository.GetTeamSeasonAveragesByTeamAsync(teamId);
    }
    
    public async Task<IEnumerable<TeamSeasonAverage>> GetTeamSeasonAveragesBySeasonAsync(int seasonId)
    {
        return await teamSeasonAverageRepository.GetTeamSeasonAveragesBySeasonAsync(seasonId);
    }
}