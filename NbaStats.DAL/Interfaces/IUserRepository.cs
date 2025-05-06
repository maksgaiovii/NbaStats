using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> AuthenticateAsync(string email);
}