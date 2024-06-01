﻿using finebe.webapi.Src.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        throw new NotImplementedException("wowa");
        var user = await _userManager.GetUserAsync(User);
        return Ok($"Hello .Net Devs! I'm {user.UserName}");
    }
}
