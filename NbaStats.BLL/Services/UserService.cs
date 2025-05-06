using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class UserService : Service<User>, IUserService
{
    private readonly IUserRepository repository;
    public UserService(IUserRepository repository) : base(repository)
    {
        this.repository = repository;
    }

    public async Task<User?> AuthenticateAsync(string email)
    {
        return await  repository.AuthenticateAsync(email);
    }
}