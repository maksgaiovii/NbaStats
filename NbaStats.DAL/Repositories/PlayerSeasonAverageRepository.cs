using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class PlayerSeasonAverageRepository : BaseRepository<PlayerSeasonAverage>, IPlayerSeasonAverageRepository
{
    public PlayerSeasonAverageRepository(DbContext context) : base(context)
    {
    }

    public async  Task<bool> UpdateStatAsync(int playerSeasonAverageId, Expression<Func<PlayerSeasonAverage, double>> statSelector, double newValue)
    {
        
        var playerSeasonAverage = await context.Set<PlayerSeasonAverage>().FindAsync(playerSeasonAverageId);
        if (playerSeasonAverage == null)
            return false;

        var memberExpression = (MemberExpression)statSelector.Body;
        var propertyName = memberExpression.Member.Name;
    
        var property = typeof(PlayerSeasonAverage).GetProperty(propertyName);
        if (property == null)
            return false;
        
        property.SetValue(playerSeasonAverage, newValue);
    
        return await context.SaveChangesAsync() > 0;
    }

    public  async Task<IEnumerable<PlayerSeasonAverage>> GetPlayerSeasonAveragesByPlayerAsync(int playerId)
    {
        return await dbSet.Where(psa => psa.PlayerId == playerId).OrderByDescending(psa => psa.Season.Year).Take(1).ToListAsync();
    }

    public  async Task<IEnumerable<PlayerSeasonAverage>> GetPlayerSeasonAveragesBySeasonAsync(int seasonId, int playerId)
    {
        return await dbSet.Where(psa => psa.PlayerId == playerId && psa.SeasonId==seasonId).ToListAsync();
    }
}