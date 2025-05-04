using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStats.BLL.Interfaces;

namespace NbaStats.UAL.Pages;

public class PlayersModel : PageModel
{
    private readonly IPlayerService _playerService;
    private const int PageSize = 10;

    public List<DAL.Data.Player> Players { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    public PlayersModel(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    public async Task OnGetAsync([FromQuery] int page = 1, [FromQuery] string searchName = null)
    {
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