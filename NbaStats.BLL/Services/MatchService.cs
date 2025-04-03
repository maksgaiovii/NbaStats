using NbaStats.BLL.DTOs;
using NbaStats.BLL.Interfaces;
using NbaStats.BLL.Mappers;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class MatchService : Service<Match>, IMatchService
{
   private readonly IMatchRepository matchRepository;
    private readonly ITeamRepository teamRepository;
    private readonly ISeasonRepository seasonRepository;
    
    public MatchService(
        IMatchRepository matchRepository, 
        ITeamRepository teamRepository, 
        ISeasonRepository seasonRepository) : base(matchRepository)
    {
        this.matchRepository = matchRepository;
        this.teamRepository = teamRepository;
        this.seasonRepository = seasonRepository;
    }
    
    public new async Task<MatchDto?> GetByIdAsync(int id)
    {
        var match = await matchRepository.GetByIdAsync(id);
        return match == null ? null : MatchDtoMapper.ToMatchDto(match);
    }
    
    public new async Task<IEnumerable<MatchDto>> GetAllAsync()
    {
        var matches = await matchRepository.GetAllAsync();
        return matches.Select(MatchDtoMapper.ToMatchDto);
    }
    
    public async Task<bool> AddAsync(MatchDto entity)
    {
        var homeTeam = await teamRepository.GetTeamByNameAsync(entity.HomeTeam);
        var awayTeam = await teamRepository.GetTeamByNameAsync(entity.AwayTeam);
        var season = await seasonRepository.GetSeasonByYearAsync(entity.Date.Year);
        var match = entity.ToMatch(homeTeam, awayTeam, season!);
    
        return await matchRepository.AddAsync(match);
    }
    
    public async Task<bool> UpdateAsync(MatchDto entity)
    {
        var match = await matchRepository.GetByIdAsync(entity.Id);
        if (match == null)
            return false;
    
        var homeTeam = await teamRepository.GetTeamByNameAsync(entity.HomeTeam);
        var awayTeam = await teamRepository.GetTeamByNameAsync(entity.AwayTeam);
        var season = await seasonRepository.GetSeasonByYearAsync(entity.Date.Year);
    
        if ( season == null)
            return false;
            
        match.Date = entity.Date;
        match.HomeTeamId = homeTeam.TeamId;
        match.AwayTeamId = awayTeam.TeamId;
        match.HomeScore = entity.HomeScore;
        match.AwayScore = entity.AwayScore;
        match.SeasonId = season.SeasonId;
        
        return await matchRepository.UpdateAsync(match);
    }
    
    public async Task<bool> DeleteAsync(MatchDto entity)
    {
        var match = await matchRepository.GetByIdAsync(entity.Id);
        if (match == null)
            return false;
    
        return await matchRepository.DeleteAsync(match);
    }
    
    public async Task<IEnumerable<MatchDto>> GetMatchesPlayedLastNightAsync()
    {
        var lastNightMatches =  await matchRepository.GetMatchesPlayedLastNightAsync();
    
        return lastNightMatches.Select(MatchDtoMapper.ToMatchDto);
    }
    
    public async Task<IEnumerable<MatchDto>> GetMatchesPlayedBySeasonAsync(int seasonId)
    {
        var seasonMatches = await matchRepository.GetMatchesPlayedBySeasonAsync(seasonId);
    
        return seasonMatches.Select(MatchDtoMapper.ToMatchDto);
    }
    
    public async Task<IEnumerable<MatchDto>> GetMatchesPlayedByTeamAsync(int teamId)
    {
        var teamMatches = await matchRepository.GetMatchesPlayedByTeamAsync(teamId);
    
        return teamMatches.Select(MatchDtoMapper.ToMatchDto);
    }
    
    public async Task<IEnumerable<MatchDto>> GetMatchesPlayedByPlayerAsync(int playerId)
    {
        
        var matches = await matchRepository.GetMatchesPlayedByPlayerAsync(playerId);
        
        return matches.Select(MatchDtoMapper.ToMatchDto);
    }
    
    public async Task<IEnumerable<MatchDto>> GetMatchesPlayedByPlayerInSeasonAsync(int playerId, int seasonId)
    {
        var matches = await matchRepository.GetMatchesPlayedByPlayerInSeasonAsync(playerId, seasonId);
        
        return matches.Select(MatchDtoMapper.ToMatchDto);
    }
    
    public async Task<IEnumerable<MatchDto>> GetMatchesPlayedByTeamInSeasonAsync(int teamId, int seasonId)
    {
        var matches = await matchRepository.GetMatchesPlayedByTeamInSeasonAsync(teamId, seasonId);
    
        return matches.Select(MatchDtoMapper.ToMatchDto);
    }
}