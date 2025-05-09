﻿using NbaStats.DAL.Data;

namespace NbaStats.DAL.Interfaces;

public interface IUserPreferencesRepository : IRepository<UserPreference>
{
    Task<UserPreference?> GetByUserIdAsync(int userId);

}