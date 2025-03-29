using Microsoft.EntityFrameworkCore;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.DAL.Repositories;

public class SeasonRepository:  BaseRepository<Season>, ISeasonRepository
{
    public SeasonRepository(DbContext context) : base(context)
    {
    }
}