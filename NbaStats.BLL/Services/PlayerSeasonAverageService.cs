using System.Linq.Expressions;
using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class PlayerSeasonAverageService : Service<PlayerSeasonAverage>, IPlayerSeasonAverageService
{
    private readonly IPlayerSeasonAverageRepository playerSeasonAverageRepository;
    
    public PlayerSeasonAverageService(IPlayerSeasonAverageRepository repository) : base(repository)
    {
        playerSeasonAverageRepository = repository;
    }
    
    public async Task<bool> UpdateStatAsync(int playerSeasonAverageId, Expression<Func<PlayerSeasonAverage, double>> statSelector, double newValue)
    {
        return await playerSeasonAverageRepository.UpdateStatAsync(playerSeasonAverageId, statSelector, newValue);
    }
    
    public async Task<IEnumerable<PlayerSeasonAverage>> GetPlayerSeasonAveragesByPlayerAsync(int playerId)
    {
        return await playerSeasonAverageRepository.GetPlayerSeasonAveragesByPlayerAsync(playerId);
    }
    
    public async Task<IEnumerable<PlayerSeasonAverage>> GetPlayerSeasonAveragesBySeasonAsync(int seasonId, int playerId)
    {
        return await playerSeasonAverageRepository.GetPlayerSeasonAveragesBySeasonAsync(seasonId, playerId);
    }
}