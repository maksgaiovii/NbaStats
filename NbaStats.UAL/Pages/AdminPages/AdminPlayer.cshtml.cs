using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;

namespace NbaStats.UAL.Pages;

public class AdminPlayerModel : PageModel
{
    private readonly IPlayerService _playerService;
    private readonly IPlayerStatService _playerStatService;

    public AdminPlayerModel(IPlayerService playerService, IPlayerStatService playerStatService)
    {
        _playerService = playerService;
        _playerStatService = playerStatService;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public DAL.Data.Player? Player { get; set; }

    [BindProperty]
    public List<PlayerStat> LastMatches { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        Player = await _playerService.GetByIdAsync(Id);
        if (Player == null)
            return NotFound();

        LastMatches = (await _playerStatService.GetPlayerStatsByPlayerAsync(Id)).Take(5).ToList();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        foreach (var stat in LastMatches)
        {
            await _playerStatService.UpdateAsync(stat);
        }

        return RedirectToPage(new { id = Id });
    }
}
