using NbaStats.BLL.DTOs;
using NbaStats.DAL.Data;

namespace NbaStats.Mappers;

public static class MatchDtoMapper
{
    public static MatchDto ToMatchDto(Match match)
    {
            
        return new MatchDto
        {
            Id = match.MatchId,
            Date = match.Date,
            HomeTeam = match.HomeTeam.Name,
            AwayTeam = match.AwayTeam.Name,
            HomeScore = match.HomeScore,
            AwayScore = match.AwayScore,
        };
    }
    
    
    public static Match ToMatch(this MatchDto matchDto, Team homeTeam, Team awayTeam, Season season)
    {
            
        return new Match
        {
            MatchId = matchDto.Id,
            Date = matchDto.Date,
            HomeTeamId = homeTeam.TeamId,
            AwayTeamId = awayTeam.TeamId,
            HomeScore = matchDto.HomeScore,
            AwayScore = matchDto.AwayScore,
            SeasonId = season.SeasonId,
            AwayTeam = awayTeam,
            HomeTeam = homeTeam,
            Season = season
        };
    }
}