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
        var lastNight = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1).Date);
        return await dbSet.Where(m => m.Date >= lastNight).ToListAsync();
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
        //TODO: Implement when TeamRepository is implemented
        
        throw new NotImplementedException();    
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedByPlayerInSeasonAsync(int playerId, int seasonId)
    {
        //TODO: Implement when TeamRepository and SeasonRepository is implemented
        throw new NotImplementedException();
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedByTeamInSeasonAsync(int teamId, int seasonId)
    {
        return await dbSet.Where(m => m.SeasonId == seasonId && (m.HomeTeamId == teamId || m.AwayTeamId == teamId)).ToListAsync();
    }
}