﻿using finebe.webapi.Src.Persistence.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finebe.webapi.Src.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GreetingController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GreetingController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetGreeting()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            return Ok($"Hello .Net Devs! I'm {user.UserName}");
        }
        catch (System.Exception)
        {
            throw;
        }

    }
}
