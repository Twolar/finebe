namespace finebe_api.Controllers;
using Microsoft.AspNetCore.Mvc;
using finebe_api.Interfaces;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
}
