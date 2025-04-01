using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class PlayerStatRepository : BaseRepository<PlayerStat>, IPlayerStatRepository
{
    public PlayerStatRepository(DbContext context) : base(context)
    {
    }

    public async Task<bool> UpdateStatAsync(int playerStatId, Expression<Func<PlayerStat, double>> statSelector, double newValue)
    {
        var playerStat = await context.Set<PlayerStat>().FindAsync(playerStatId);
        if (playerStat == null)
            return false;

        var memberExpression = (MemberExpression)statSelector.Body;
        var propertyName = memberExpression.Member.Name;
    
        var property = typeof(PlayerStat).GetProperty(propertyName);
        if (property == null)
            return false;
        
        property.SetValue(playerStat, newValue);
    
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<PlayerStat>> GetPlayerStatsByGameAsync(int gameId)
    {
        return await dbSet.Where(ps => ps.MatchId == gameId).ToListAsync();
    }
    
    public async Task<IEnumerable<PlayerStat>> GetPlayerStatsByPlayerAsync(int playerId)
    {
        return await dbSet.Where(ps => ps.PlayerId == playerId).ToListAsync();
    }
}

