using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class UserPreferencesRepository : BaseRepository<UserPreference>, IUserPreferencesRepository
{
    private readonly DbContext context;
    public UserPreferencesRepository(DbContext context) : base(context)
    {
        this.context=context;
    }

    public async Task<UserPreference?> GetByUserIdAsync(int userId)
    {
        return await context.Set<UserPreference>()
            .AsNoTracking()
            .FirstOrDefaultAsync(up => up.Userid == userId);
    }
}