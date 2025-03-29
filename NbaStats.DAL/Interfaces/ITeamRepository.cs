using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface ITeamRepository : IRepository<Team>, ITeamStatRepository
{
    Task<IEnumerable<Team>> GetTeamsByDivisionAsync(string divisionName);
    
    Task<IEnumerable<Team>> GetTeamsByConferenceAsync(string conferenceName);
}