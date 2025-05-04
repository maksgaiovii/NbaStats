using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
{
    public PlayerRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Player>> GetPlayersByNameAsync()
    {
        return await dbSet.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<IEnumerable<Player>> GetPlayersByLastNameAsync()
    {
        return await dbSet.OrderBy(p => p.Surname).ToListAsync();
    }

    public async Task<IEnumerable<Player>> GetPlayersByTeamAsync(int teamId)
    {
        return await dbSet.Where(p => p.TeamId == teamId).ToListAsync();
    }

    public async Task<IEnumerable<Player>> GetPlayersByPositionAsync(string position)
    {
        return await dbSet.Where(p => p.Position == position).ToListAsync();
    }

    public async Task<IEnumerable<Player>> GetPlayersByHeightAsync(int height)
    {
        return await dbSet.Where(p => p.Height == height).ToListAsync();
    }

    public async Task<IEnumerable<Player>> GetPlayersByWeightAsync(int weight)
    {
        return await dbSet.Where(p => p.Weight == weight).ToListAsync();
    }

    public async Task<IEnumerable<Player>> GetPlayersByAgeAsync(int age)
    {
        return await dbSet.Where(p =>
                DateTime.Today.Year - p.BirthDate.Year -
                (p.BirthDate.Date > DateTime.Today.AddYears(-DateTime.Today.Year + p.BirthDate.Year) ? 1 : 0) == age)
            .ToListAsync();
    }

    public async Task<IEnumerable<Player>> GetPlayersWithMostStatAverageAsync(int topN,
        Expression<Func<PlayerSeasonAverage, double>> statSelector)
    {
        return (await context.Set<PlayerSeasonAverage>()
            .OrderByDescending(statSelector)
            .Take(topN)
            .Select(psa => psa.Player)
            .ToListAsync())!;
    }

    public async Task<IEnumerable<Player>> GetPlayersWithMostStatAsync(int topN,
        Expression<Func<PlayerStat, double>> statSelector)
    {
        return (await context.Set<PlayerStat>()
            .OrderByDescending(statSelector)
            .Take(topN)
            .Select(ps => ps.Player)
            .ToListAsync())!;
    }

    public async Task<IEnumerable<Player>> GetAllWithTeamAsync()
    {
        return await dbSet.Include(p => p.Team)
            .ToListAsync();
    }
}