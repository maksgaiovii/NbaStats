using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NbaStats.UAL.Pages;

public class LogoutModel : PageModel
{
    public IActionResult OnPost()
    {
        IsSignedInHelper.IsSignedIn = false; // Set your flag here
        return RedirectToPage("/Index"); // Redirect to Index
    }
}