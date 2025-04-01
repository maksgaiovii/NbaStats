using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class TeamSeasonAverageRepository : BaseRepository<TeamSeasonAverage>, ITeamSeasonAverageRepository
{
    public TeamSeasonAverageRepository(DbContext context) : base(context)
    {
    }

    public async Task<bool> UpdateStatAsync(int teamSeasonAverageId, Expression<Func<TeamSeasonAverage, double>> statSelector, double newValue)
    {
        var teamSeasonAverage = await context.Set<TeamSeasonAverage>().FindAsync(teamSeasonAverageId);
        if (teamSeasonAverage == null)
            return false;
    
        var memberExpression = (MemberExpression)statSelector.Body;
        var propertyName = memberExpression.Member.Name;
        
        var property = typeof(TeamSeasonAverage).GetProperty(propertyName);
        if (property == null)
            return false;
            
        property.SetValue(teamSeasonAverage, newValue);
        
        return await context.SaveChangesAsync() > 0;
    }
    
    public async Task<IEnumerable<TeamSeasonAverage>> GetTeamSeasonAveragesByTeamAsync(int teamId)
    {
        return await dbSet.Where(tsa => tsa.TeamId == teamId).OrderByDescending(tsa => tsa.Season.Year).Take(1).ToListAsync();
    }
    
    public async Task<IEnumerable<TeamSeasonAverage>> GetTeamSeasonAveragesBySeasonAsync(int seasonId)
    {
        return await dbSet.Where(tsa => tsa.SeasonId == seasonId).ToListAsync();
    }
}