using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class UserPreferencesRepository : BaseRepository<UserPreference>, IUserPreferencesRepository
{
    public UserPreferencesRepository(DbContext context) : base(context)
    {
    }
}