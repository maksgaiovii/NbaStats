using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface ITeamStatRepository
{
    Task<IEnumerable<Team>> GetTeamWithMostPointsAsync(int topN);
    Task<IEnumerable<Team>> GetTeamWithMostReboundsAsync(int topN);
    Task<IEnumerable<Team>> GetTeamWithMostAssistsAsync(int topN);
    Task<IEnumerable<Team>> GetTeamWithMostStealsAsync(int topN);
    Task<IEnumerable<Team>> GetTeamWithMostBlocksAsync(int topN);
    Task<IEnumerable<Team>> GetTeamWithMostTurnoversAsync(int topN);
    Task<IEnumerable<Team>> GetTeamWithMostPersonalFoulsAsync(int topN);
    Task<IEnumerable<Team>> GetTeamWithMostFieldGoalsMadeAsync(int topN);
    Task<IEnumerable<Team>> GetTeamWithMostFieldGoalsAttemptedAsync(int topN);
    Task<IEnumerable<Team>> GetTeamWithMostThreePointFieldGoalsMadeAsync(int topN);
    Task<IEnumerable<Team>> GetTeamWithMostThreePointFieldGoalsAttemptedAsync(int topN);
}