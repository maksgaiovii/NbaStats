using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStats.BLL.Interfaces;

namespace NbaStats.UAL.Pages;

public class AdminTeam : PageModel
{
    private readonly ITeamService _teamService;

    public AdminTeam(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [BindProperty]
    public int TeamId { get; set; }

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public string City { get; set; } = string.Empty;

    [BindProperty]
    public string Conference { get; set; } = string.Empty;

    [BindProperty]
    public string Division { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {
        if (!int.TryParse(Request.Query["teamId"], out var teamId))
            return NotFound();

        var team = await _teamService.GetByIdAsync(teamId);
        if (team == null)
            return NotFound();

        TeamId = team.TeamId;
        Name = team.Name;
        City = team.City;
        Conference = team.Conference;
        Division = team.Division;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var team = await _teamService.GetByIdAsync(TeamId);
        if (team == null)
            return NotFound();

        team.Name = Name;
        team.City = City;
        team.Conference = Conference;
        team.Division = Division;

        await _teamService.UpdateAsync(team);

        return RedirectToPage("/AdminPages/AdminTeam", new { teamId = TeamId });
    }
}