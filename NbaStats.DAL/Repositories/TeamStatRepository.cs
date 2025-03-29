using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class TeamStatRepository : ITeamStatRepository
{
    private readonly DbContext context;

    public TeamStatRepository(DbContext context)
    {
        this.context = context;
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
}