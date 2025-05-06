using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStats.BLL.Interfaces;

namespace NbaStats.UAL.Pages;

public class Login : PageModel
{
    private readonly IUserService _userService;

    public Login(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty] public InputModel Input { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public class InputModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }

    public void OnGet()
    {
        // No logic needed for GET
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Invalid input.";
            return Page();
        }

        var user = await _userService.AuthenticateAsync(Input.Email);
        if (user == null)
        {
            ErrorMessage = $"Email '{Input.Email}' is not registered, please try to register instead.";
            return Page();
        }

        IsSignedInHelper.IsSignedIn = true;
        IsSignedInHelper.userId = user.UserId;
        if (user.Role == "Admin")
        {
            return RedirectToPage("/AdminPages/AdminDashboard");
        }

        return RedirectToPage("/Index");
    }
}