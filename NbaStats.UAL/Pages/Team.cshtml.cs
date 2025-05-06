using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;

namespace NbaStats.UAL.Pages;

public class TeamModel : PageModel
{
    private readonly IUserPreferenceService UserPreferencesService;
    [BindProperty]
    public int TeamId { get; set; }

    public TeamModel(IUserPreferenceService userPreferencesService)
    {
        UserPreferencesService = userPreferencesService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (IsSignedInHelper.IsSignedIn)
        {
            int userId = IsSignedInHelper.userId;
            var userPreferences = await UserPreferencesService.GetByUserIdAsync(userId);
            if (userPreferences != null)
            {
                userPreferences.TeamId = TeamId;
                await UserPreferencesService.UpdateAsync(userPreferences);
                return RedirectToPage("/Team", new { teamId = TeamId });
            }
            var preferences = new UserPreference
            {
                UserPreferencesId = 0,
                TeamId = TeamId,
                Userid = (int)IsSignedInHelper.userId,
                PlayerId = 17,
                Player = null,
                Team = null,
                User = null,
            };
            await UserPreferencesService.AddAsync(preferences);
            return RedirectToPage("/Team", new { teamId = TeamId });
        }

        return Unauthorized();
    }

    public void OnGet()
    {

    }
}