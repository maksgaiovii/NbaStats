namespace NbaStats.DAL.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}