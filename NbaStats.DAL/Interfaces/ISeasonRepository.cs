using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface ISeasonRepository : IRepository<Season>
{
    Task<Season?> GetSeasonByYearAsync(int year);
}