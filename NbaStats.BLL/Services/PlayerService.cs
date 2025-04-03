using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;
using NbaStats.DAL.Interfaces;

namespace NbaStats.BLL.Services;

public class PlayerService : Service<Player>, IPlayerService
{

    public PlayerService(IPlayerRepository playerRepository) : base(playerRepository)
    {
    }

}