﻿using NbaStats.DAL.Data;

namespace NbaStats.BLL.Interfaces;

public interface IUserPreferenceService : IService<UserPreference>
{
    Task<UserPreference?> GetByUserIdAsync(int userId);
}