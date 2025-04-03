using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Interfaces;
using NbaStats.DAL.Data;

namespace NbaStats.BLL.Services;

public class PlayerStatService : Service<PlayerStat>, IPlayerStatService
{
    private readonly IPlayerStatRepository playerStatRepository;
    public PlayerStatService(IPlayerStatRepository repository) : base(repository)
        {
            playerStatRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    
    
    public async Task<IEnumerable<PlayerStat>> GetPlayerStatsByGameAsync(int gameId)
    {
        return await playerStatRepository.GetPlayerStatsByGameAsync(gameId);
    }
    
    public async Task<IEnumerable<PlayerStat>> GetPlayerStatsByPlayerAsync(int playerId)
    {
        return await playerStatRepository.GetPlayerStatsByPlayerAsync(playerId);
    }
    
   
}