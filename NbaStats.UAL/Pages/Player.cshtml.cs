using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;

namespace NbaStats.UAL.Pages;

public class PlayerModel : PageModel
{
    
    private readonly IUserPreferenceService UserPreferencesService;
    [BindProperty]
    public int PlayerId { get; set; }
    public PlayerModel(IUserPreferenceService userPreferencesService)
    {
        UserPreferencesService = userPreferencesService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (IsSignedInHelper.IsSignedIn)
        {
            // Get preferences for the current user by userId
            int userId = IsSignedInHelper.userId;
            var userPreferences = await UserPreferencesService.GetByUserIdAsync(userId);
            if (userPreferences != null)
            {
                userPreferences.PlayerId = PlayerId;
                await UserPreferencesService.UpdateAsync(userPreferences);
                return RedirectToPage("/Player", new { id = PlayerId });
            }
            var preferences = new UserPreference
            {
                UserPreferencesId = 0,
                PlayerId = PlayerId,
                Userid = (int)IsSignedInHelper.userId,
                TeamId = 1610612737,
                Player = null,
                Team = null,
                User = null,
            };
            await UserPreferencesService.AddAsync(preferences);
            return RedirectToPage("/Player", new { id = PlayerId });
        }

        return Unauthorized();
    }

    
    public void OnGet()
    {
        
    }
    
    
}