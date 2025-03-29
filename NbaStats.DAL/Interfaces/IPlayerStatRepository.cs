using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface IPlayerStatRepository
{
    Task<IEnumerable<Player>> GetPlayersWithMostPointsAsync(int topN);
    Task<IEnumerable<Player>> GetPlayersWithMostReboundsAsync(int topN);
    Task<IEnumerable<Player>> GetPlayersWithMostAssistsAsync(int topN);
    Task<IEnumerable<Player>> GetPlayersWithMostStealsAsync(int topN);
    Task<IEnumerable<Player>> GetPlayersWithMostBlocksAsync(int topN);
    Task<IEnumerable<Player>> GetPlayersWithMostTurnoversAsync(int topN);
    Task<IEnumerable<Player>> GetPlayersWithMostPersonalFoulsAsync(int topN);
    Task<IEnumerable<Player>> GetPlayersWithMostFieldGoalsMadeAsync(int topN);
    Task<IEnumerable<Player>> GetPlayersWithMostFieldGoalsAttemptedAsync(int topN);
    Task<IEnumerable<Player>> GetPlayersWithMostThreePointFieldGoalsMadeAsync(int topN);
    Task<IEnumerable<Player>> GetPlayersWithMostThreePointFieldGoalsAttemptedAsync(int topN);
}