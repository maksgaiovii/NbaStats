using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface IMatchRepository : IRepository<Match>
{
    public Task<IEnumerable<Match>> GetMatchesPlayedLastNightAsync();
    
    public Task<IEnumerable<Match>> GetMatchesPlayedBySeasonAsync(int seasonId);
    
    public Task<IEnumerable<Match>> GetMatchesPlayedByTeamAsync(int teamId);
    
    public Task<IEnumerable<Match>> GetMatchesPlayedByPlayerAsync(int playerId);
    
    public Task<IEnumerable<Match>> GetMatchesPlayedByPlayerInSeasonAsync(int playerId, int seasonId);
    
    public Task<IEnumerable<Match>> GetMatchesPlayedByTeamInSeasonAsync(int teamId, int seasonId);
    
    public Task<IEnumerable<Match>> GetAllWithTeamsAsync();
    
}