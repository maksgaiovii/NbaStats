using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class SeasonService : Service<Season>, ISeasonService
{
    private readonly ISeasonRepository seasonRepository;
    
    public SeasonService(ISeasonRepository repository) : base(repository)
    {
        seasonRepository = repository;
    }

    public async Task<Season?> GetSeasonByYearAsync(int year)
    {
        return await seasonRepository.GetSeasonByYearAsync(year);
    }
}