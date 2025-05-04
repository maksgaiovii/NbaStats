using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class PlayerService : Service<Player>, IPlayerService
{

    private readonly IPlayerRepository playerRepository;
    public PlayerService(IPlayerRepository playerRepository) : base(playerRepository)
    {
        this.playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
    }

    public async Task<IEnumerable<Player>> GetPlayersByTeamAsync(int teamId)
    {
        return await playerRepository.GetPlayersByTeamAsync(teamId);
    }
    
    public async Task<IEnumerable<Player>> GetPlayersByNameAsync()
        {
            return await playerRepository.GetPlayersByNameAsync();
        }
    
        public async Task<IEnumerable<Player>> GetPlayersByLastNameAsync()
        {
            return await playerRepository.GetPlayersByLastNameAsync();
        }
    
        public async Task<IEnumerable<Player>> GetPlayersByPositionAsync(string position)
        {
            return await playerRepository.GetPlayersByPositionAsync(position);
        }
    
        public async Task<IEnumerable<Player>> GetPlayersByHeightAsync(int height)
        {
            return await playerRepository.GetPlayersByHeightAsync(height);
        }
    
        public async Task<IEnumerable<Player>> GetPlayersByWeightAsync(int weight)
        {
            return await playerRepository.GetPlayersByWeightAsync(weight);
        }
    
        public async Task<IEnumerable<Player>> GetPlayersByAgeAsync(int age)
        {
            return await playerRepository.GetPlayersByAgeAsync(age);
        }
    
        public async Task<IEnumerable<Player>> GetPlayersWithMostStatAverageAsync(int topN, System.Linq.Expressions.Expression<Func<PlayerSeasonAverage, double>> statSelector)
        {
            return await playerRepository.GetPlayersWithMostStatAverageAsync(topN, statSelector);
        }
    
        public async Task<IEnumerable<Player>> GetPlayersWithMostStatAsync(int topN, System.Linq.Expressions.Expression<Func<PlayerStat, double>> statSelector)
        {
            return await playerRepository.GetPlayersWithMostStatAsync(topN, statSelector);
        }

        public async  Task<IEnumerable<Player>> GetAllWithTeamAsync()
        {
            return await playerRepository.GetAllWithTeamAsync();
        }
}