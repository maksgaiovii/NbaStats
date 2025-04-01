using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class TeamStatRepository : BaseRepository<TeamStat>, ITeamStatRepository
{
    public TeamStatRepository(DbContext context) : base(context)
    {
    }

    public async Task<bool> UpdateStatAsync(int teamStatId, Expression<Func<TeamStat, double>> statSelector, double newValue)
    {
        var teamStat = await context.Set<TeamStat>().FindAsync(teamStatId);
        if (teamStat == null)
            return false;

        var memberExpression = (MemberExpression)statSelector.Body;
        var propertyName = memberExpression.Member.Name;
    
        var property = typeof(TeamStat).GetProperty(propertyName);
        if (property == null)
            return false;
        
        property.SetValue(teamStat, newValue);
    
        return await context.SaveChangesAsync() > 0;  
    }

    public async Task<IEnumerable<TeamStat>> GetTeamStatsByGameAsync(int gameId)
    {
        return await dbSet.Where(ts => ts.MatchId == gameId).ToListAsync();
    }
}