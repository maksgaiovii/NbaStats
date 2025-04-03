using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class TeamRepository : BaseRepository<Team>, ITeamRepository
{
    public TeamRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Team>> GetTeamWithMostStatAsync(int topN, Expression<Func<TeamStat, int>> statSelector)
    {
        var teamIds = await context.Set<TeamStat>()
            .OrderByDescending(statSelector)
            .Take(topN)
            .Select(t => t.TeamId)
            .ToListAsync();

        return await context.Set<Team>()
            .Where(t => teamIds.Contains(t.TeamId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Team>> GetTeamsWithMostStatAverageAsync(int topN, Expression<Func<TeamSeasonAverage, double>> statSelector)
    {
        var teamIds = await context.Set<TeamSeasonAverage>()
            .OrderByDescending(statSelector)
            .Take(topN)
            .Select(t => t.TeamId)
            .ToListAsync();

        return await context.Set<Team>()
            .Where(t => teamIds.Contains(t.TeamId))
            .ToListAsync();
    }

    public async Task<Team> GetTeamByNameAsync(string teamName)
    {
        return await context.Set<Team>()
            .FirstOrDefaultAsync(t => t.Name!.ToLower() == teamName.ToLower()) ?? throw new InvalidOperationException();
    }

    public async Task<IEnumerable<Team>> GetTeamsByDivisionAsync(string divisionName)
    {
        return await context.Set<Team>()
            .Where(t => t.Division!.ToLower() == divisionName.ToLower())
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Team>> GetTeamsByConferenceAsync(string conferenceName)
    {
        return await context.Set<Team>()
            .Where(t => t.Conference!.ToLower() == conferenceName.ToLower())
            .ToListAsync();
    }
}