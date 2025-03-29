using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface ITeamSeasonAverageRepository : IRepository<TeamSeasonAverage>
{
    Task<TeamSeasonAverage?> GetTeamSeasonAverageAsync(int teamId, int seasonId);

    Task<IEnumerable<Team>> GetTeamsWithMostPointsAverageAsync(int topN);

    Task<IEnumerable<Team>> GetTeamsWithMostReboundsAverageAsync(int topN);

    Task<IEnumerable<Team>> GetTeamsWithMostAssistsAverageAsync(int topN);

    Task<IEnumerable<Team>> GetTeamsWithMostStealsAverageAsync(int topN);

    Task<IEnumerable<Team>> GetTeamsWithMostBlocksAverageAsync(int topN);

    Task<IEnumerable<Team>> GetTeamsWithMostTurnoversAverageAsync(int topN);

    Task<IEnumerable<Team>> GetTeamsWithMostPersonalFoulsAverageAsync(int topN);

    Task<IEnumerable<Team>> GetTeamsWithMostFieldGoalsMadeAverageAsync(int topN);

    Task<IEnumerable<Team>> GetTeamsWithMostFieldGoalsAttemptedAverageAsync(int topN);

    Task<IEnumerable<Team>> GetTeamsWithMostThreePointFieldGoalsMadeAverageAsync(int topN);

    Task<IEnumerable<Team>> GetTeamsWithMostThreePointFieldGoalsAttemptedAverageAsync(int topN);
}