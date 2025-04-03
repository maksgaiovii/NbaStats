using NbaStats.BLL.DTOs;

namespace NbaStats.BLL.Interfaces;

public interface IMatchService : IService<MatchDto>
{
    public Task<IEnumerable<MatchDto>> GetMatchesPlayedLastNightAsync();
    
    public Task<IEnumerable<MatchDto>> GetMatchesPlayedBySeasonAsync(int seasonId);
    
    public Task<IEnumerable<MatchDto>> GetMatchesPlayedByTeamAsync(int teamId);
    
    public Task<IEnumerable<MatchDto>> GetMatchesPlayedByPlayerAsync(int playerId);
    
    public Task<IEnumerable<MatchDto>> GetMatchesPlayedByPlayerInSeasonAsync(int playerId, int seasonId);
    
    public Task<IEnumerable<MatchDto>> GetMatchesPlayedByTeamInSeasonAsync(int teamId, int seasonId);
}