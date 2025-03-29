using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class PlayerStatRepository : IPlayerStatRepository
{
    private readonly DbContext context;

    public PlayerStatRepository(DbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Player>> GetPlayersWithMostStatAsync(int topN, Expression<Func<PlayerStat, int>> statSelector)
    {
        var playerIds = await context.Set<PlayerStat>()
            .OrderByDescending(statSelector)
            .Take(topN)
            .Select(p => p.PlayerId)
            .ToListAsync();

        return await context.Set<Player>()
            .Where(p => playerIds.Contains(p.PlayerId))
            .ToListAsync();
    }
}