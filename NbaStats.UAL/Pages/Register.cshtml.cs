using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStats.BLL.Interfaces;
using NbaStats.DAL.Data;

namespace NbaStats.UAL.Pages;

public class Register : PageModel
{
    private readonly IUserService _userService;

    public Register(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty] public InputModel Input { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Invalid input.";
            return Page();
        }

        var existingUser = await _userService.AuthenticateAsync(Input.Email);
        if (existingUser != null)
        {
            ErrorMessage = $"Email '{Input.Email}' is already registered.";
            return Page();
        }

        var role = Input.IsAdmin ? "Admin" : "User";
        var user = new User
        {
            Email = Input.Email,
            Role = role
        };
        await _userService.AddAsync(user);

        return RedirectToPage("/Login");
    }
}
public static class IsSignedInHelper
{
    public static bool IsSignedIn = false;
    public static int userId;
}