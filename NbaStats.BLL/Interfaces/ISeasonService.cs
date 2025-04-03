using NbaStats.DAL.Data;

namespace NbaStats.BLL.Interfaces;

public interface ISeasonService : IService<Season>
{
    Task<Season?> GetSeasonByYearAsync(int year);
}