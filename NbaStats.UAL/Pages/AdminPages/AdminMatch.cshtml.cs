using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;

namespace NbaStats.UAL.Pages;

public class AdminMatchModel : PageModel
{
    private readonly IPlayerStatService _playerStatService;
    private readonly IPlayerService _playerService;

    public AdminMatchModel(IPlayerStatService playerStatService, IPlayerService playerService)
    {
        _playerStatService = playerStatService;
        _playerService = playerService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; } // MatchId from query string

    public List<PlayerStat> PlayerStats { get; set; } = new();
    public Dictionary<int, string> PlayerNames { get; set; } = new();

    public async Task OnGetAsync()
    {
        PlayerStats = (List<PlayerStat>)await _playerStatService.GetPlayerStatsByGameAsync(Id);

        var playerIds = PlayerStats.Select(s => s.PlayerId).Distinct();
        foreach (var pid in playerIds)
        {
            var player = await _playerService.GetByIdAsync(pid);
            PlayerNames[pid] = player.Name;
        }
    }

    public async Task<IActionResult> OnPostUpdateAsync()
    {
        var statIdStr = Request.Form["StatId"];
        if (!int.TryParse(statIdStr, out var statId))
            return BadRequest("Invalid StatId");

        var stat = await _playerStatService.GetByIdAsync(statId);
        if (stat == null) return NotFound();

        // Parse fields from form
        stat.Points = int.TryParse(Request.Form["Points"], out var p) ? p : 0;
        stat.Rebounds = int.TryParse(Request.Form["Rebounds"], out var r) ? r : 0;
        stat.Assists = int.TryParse(Request.Form["Assists"], out var a) ? a : 0;
        stat.Steals = int.TryParse(Request.Form["Steals"], out var s) ? s : 0;
        stat.Blocks = int.TryParse(Request.Form["Blocks"], out var b) ? b : 0;
        stat.Turnovers = int.TryParse(Request.Form["Turnovers"], out var t) ? t : 0;
        stat.FgMade = int.TryParse(Request.Form["FgMade"], out var fgm) ? fgm : 0;
        stat.FgAttempted = int.TryParse(Request.Form["FgAttempted"], out var fga) ? fga : 0;
        stat.ThreePointersMade = int.TryParse(Request.Form["ThreePointersMade"], out var tpm) ? tpm : 0;
        stat.ThreePointersAttempted = int.TryParse(Request.Form["ThreePointersAttempted"], out var tpa) ? tpa : 0;
        stat.FreeThrowsMade = int.TryParse(Request.Form["FreeThrowsMade"], out var ftm) ? ftm : 0;
        stat.FreeThrowsAttempted = int.TryParse(Request.Form["FreeThrowsAttempted"], out var fta) ? fta : 0;

        await _playerStatService.UpdateAsync(stat);

        // Redirect back to the same page with match ID
        return RedirectToPage(new { id = stat.MatchId });
    }
}
