using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using finebe_api.Models.Domain;
using finebe_api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore;

namespace finebe_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<User>), 200)]
    [ProducesResponseType(typeof(Result), 400)]
    public async Task<IActionResult> CreateUser(User user, string password)
    {
        var result = await _userService.CreateUserAsync(user, password);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpGet("ByUserId/{userId}")]
    [ProducesResponseType(typeof(Result<User>), 200)]
    [ProducesResponseType(typeof(Result), 404)]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var result = await _userService.GetUserByIdAsync(userId);
        return result.Succeeded ? Ok(result) : NotFound(result);
    }

    [HttpGet("ByUsername/{username}")]
    [ProducesResponseType(typeof(Result<User>), 200)]
    [ProducesResponseType(typeof(Result), 404)]
    public async Task<IActionResult> GetByUsername(string username)
    {
        var result = await _userService.GetByUsernameAsync(username);
        return result.Succeeded ? Ok(result) : NotFound(result);
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(Result<IEnumerable<User>>), 200)]
    [ProducesResponseType(typeof(Result), 404)]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.GetAllUsersAsync();
        return result.Succeeded ? Ok(result) : NotFound(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Result<User>), 200)]
    [ProducesResponseType(typeof(Result), 400)]
    public async Task<IActionResult> UpdateUser(Guid userId, User user)
    {
        var result = await _userService.UpdateUserAsync(userId, user);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("ByUserId/{userId}")]
    [ProducesResponseType(typeof(Result<bool>), 200)]
    [ProducesResponseType(typeof(Result), 400)]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var result = await _userService.DeleteUserAsync(userId);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
}
