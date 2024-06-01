using finebe.webapi.Src.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace finebe.webapi.Src.Controllers;

[ApiController]
[Route("[controller]")]
public class GreetingController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GreetingController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetGreeting()
    {
        try
        {
            
            throw new NotImplementedException("wowa");

        }
        catch (System.Exception ex)
        {
            throw new Exception ("test 123", ex);
        }
        var user = await _userManager.GetUserAsync(User);
        return Ok($"Hello .Net Devs! I'm {user.UserName}");
    }
}
