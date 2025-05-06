using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class UserPreferenceService : Service<UserPreference>, IUserPreferenceService
{
    private readonly IUserPreferencesRepository repository;
    public UserPreferenceService(IUserPreferencesRepository repository) : base(repository)
    {
        this.repository = repository;
    }

    public async Task<UserPreference?> GetByUserIdAsync(int userId)
    {
        return await repository.GetByUserIdAsync(userId);
    }
}