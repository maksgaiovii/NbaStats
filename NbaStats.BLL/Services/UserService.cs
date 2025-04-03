using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class UserService : Service<User>, IUserService
{
    public UserService(IUserRepository repository) : base(repository)
    {
    }
}