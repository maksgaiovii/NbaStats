using System.Linq.Expressions;
using NbaStats.DAL.Data;

namespace NbaStats.BLL.Interfaces;

public interface IPlayerService : IService<Player>
{
    Task<IEnumerable<Player>> GetPlayersByNameAsync();
    
    Task<IEnumerable<Player>> GetPlayersByLastNameAsync();

    Task<IEnumerable<Player>> GetPlayersByTeamAsync(int teamId);
    
    Task<IEnumerable<Player>> GetPlayersByPositionAsync(string position);
    
    Task<IEnumerable<Player>> GetPlayersByHeightAsync(int height);
    
    Task<IEnumerable<Player>> GetPlayersByWeightAsync(int weight);
    
    Task<IEnumerable<Player>> GetPlayersByAgeAsync(int age);
    
    Task<IEnumerable<Player>> GetPlayersWithMostStatAverageAsync(int topN,
        Expression<Func<PlayerSeasonAverage, double>> statSelector);
    
    Task<IEnumerable<Player>> GetPlayersWithMostStatAsync(int topN,
        Expression<Func<PlayerStat, double>> statSelector);
}