using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class MatchService : Service<Match>, IMatchService
{
   private readonly IMatchRepository matchRepository;
    
    public MatchService(
        IMatchRepository matchRepository) : base(matchRepository)
    {
        this.matchRepository = matchRepository;
    }
    
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedLastNightAsync()
    {
        return await matchRepository.GetMatchesPlayedLastNightAsync();
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedBySeasonAsync(int seasonId)
    {
        return await matchRepository.GetMatchesPlayedBySeasonAsync(seasonId);
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedByTeamAsync(int teamId)
    {
        return await matchRepository.GetMatchesPlayedByTeamAsync(teamId);
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedByPlayerAsync(int playerId)
    {
        return await matchRepository.GetMatchesPlayedByPlayerAsync(playerId);
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedByPlayerInSeasonAsync(int playerId, int seasonId)
    {
        return await matchRepository.GetMatchesPlayedByPlayerInSeasonAsync(playerId, seasonId);
    }
    
    public async Task<IEnumerable<Match>> GetMatchesPlayedByTeamInSeasonAsync(int teamId, int seasonId)
    {
        return await matchRepository.GetMatchesPlayedByTeamInSeasonAsync(teamId, seasonId);
    }
}