using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class MatchRepository : BaseRepository<Match>, IMatchRepository
{
    public MatchRepository(DbContext context) : base(context)
    {
    }

   public async Task<IEnumerable<Match>> GetMatchesPlayedLastNightAsync()
    {
        return await dbSet.Include(m => m.HomeTeam).Include(m => m.AwayTeam).OrderByDescending(m => m.Date).Take(10).ToListAsync();
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedBySeasonAsync(int seasonId)
    {
        return await dbSet.Where(m => m.SeasonId == seasonId).ToListAsync();
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedByTeamAsync(int teamId)
    {
        return await dbSet.Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId).ToListAsync();
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedByPlayerAsync(int playerId)
    {
        var matchIds = await context.Set<PlayerStat>()
            .Where(ps => ps.PlayerId == playerId)
            .Select(ps => ps.MatchId)
            .Distinct()
            .ToListAsync();
    
        return await dbSet
            .Where(m => matchIds.Contains(m.MatchId))
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedByPlayerInSeasonAsync(int playerId, int seasonId)
    {
        var player = await context.Set<Player>().FindAsync(playerId);
        if (player == null)
            return [];
            
        return await dbSet.Where(m => (m.HomeTeamId == player.TeamId || m.AwayTeamId == player.TeamId) && m.SeasonId==seasonId).ToListAsync();
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedByTeamInSeasonAsync(int teamId, int seasonId)
    {
        return await dbSet.Where(m => m.SeasonId == seasonId && (m.HomeTeamId == teamId || m.AwayTeamId == teamId)).ToListAsync();
    }

    public async Task<IEnumerable<Match>> GetAllWithTeamsAsync()
    {
        return await dbSet.Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .ToListAsync();
    }
}