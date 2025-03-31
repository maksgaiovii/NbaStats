using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}