using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class UserPreferenceService : Service<UserPreference>, IUserPreferenceService
{
    public UserPreferenceService(IUserPreferencesRepository repository) : base(repository)
    {
    }
}