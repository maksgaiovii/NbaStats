using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface IPlayerSeasonAverageRepository
{

    Task<IEnumerable<PlayerSeasonAverage>> GetPlayersWithMostPointsAverageAsync(int topN);

    Task<IEnumerable<PlayerSeasonAverage>> GetPlayersWithMostReboundsAverageAsync(int topN);
    
    Task<IEnumerable<PlayerSeasonAverage>> GetPlayersWithMostAssistsAverageAsync(int topN);
    
    Task<IEnumerable<PlayerSeasonAverage>> GetPlayersWithMostStealsAverageAsync(int topN);
    
    Task<IEnumerable<PlayerSeasonAverage>> GetPlayersWithMostBlocksAverageAsync(int topN);
    
    Task<IEnumerable<PlayerSeasonAverage>> GetPlayersWithMostTurnoversAverageAsync(int topN);
    
    Task<IEnumerable<PlayerSeasonAverage>> GetPlayersWithMostPersonalFoulsAverageAsync(int topN);
    
    Task<IEnumerable<PlayerSeasonAverage>> GetPlayersWithMostFieldGoalsMadeAverageAsync(int topN);
    
    Task<IEnumerable<PlayerSeasonAverage>> GetPlayersWithMostFieldGoalsAttemptedAverageAsync(int topN);
    
    Task<IEnumerable<PlayerSeasonAverage>> GetPlayersWithMostThreePointFieldGoalsMadeAverageAsync(int topN);
    
    Task<IEnumerable<PlayerSeasonAverage>> GetPlayersWithMostThreePointFieldGoalsAttemptedAverageAsync(int topN);
}