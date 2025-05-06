using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStats.BLL.Interfaces;

namespace NbaStats.UAL.Pages;

public class PlayersModel : PageModel
{
    private readonly IPlayerService _playerService;
    private readonly IUserPreferenceService userService;
    private const int PageSize = 10;

    public List<DAL.Data.Player> Players { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string FavouritePlayerName { get; set; } = string.Empty;

    public PlayersModel(IPlayerService playerService, IUserPreferenceService userService)
    {
        _playerService = playerService;
        this.userService = userService;
    }

    public async Task OnGetAsync([FromQuery] int page = 1, [FromQuery] string searchName = null)
    {
        if (IsSignedInHelper.IsSignedIn)
        {
            var userpreferences = await userService.GetByUserIdAsync(IsSignedInHelper.userId);
            if (userpreferences != null)
            {
                var favouritePlayer = _playerService.GetByIdAsync(userpreferences!.PlayerId);
                if (favouritePlayer != null)
                {
                    FavouritePlayerName = favouritePlayer.Result.Name;
                }
            }
        }
        var allPlayers = await _playerService.GetAllWithTeamAsync();
        var filteredPlayers = string.IsNullOrWhiteSpace(searchName)
            ? allPlayers
            : allPlayers.Where(p => p.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase));

        var orderedPlayers = filteredPlayers.OrderBy(p => p.Name).ToList();

        TotalPages = (int)Math.Ceiling(orderedPlayers.Count / (double)PageSize);
        CurrentPage = page;

        Players = orderedPlayers
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();
    }
}