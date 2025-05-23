﻿using NbaStats.DAL.Data;

namespace NbaStats.BLL.Interfaces;

public interface IUserService : IService<User>
{
    Task<User?> AuthenticateAsync(string email);
}