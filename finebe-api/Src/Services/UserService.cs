using AspNetCoreHero.Results;
using finebe_api.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace finebe_api.Services;

public interface IUserService
{
    Task<Result<User>> CreateUserAsync(User user, string password);
    Task<Result<User>> GetUserByIdAsync(string userId);
    Task<Result<User>> GetByUsernameAsync(string username);
    Task<Result<IEnumerable<User>>> GetAllUsersAsync();
    Task<Result<User>> UpdateUserAsync(User user);
    Task<Result<bool>> DeleteUserAsync(User user);
}

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<User>> CreateUserAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            // Retrieve the user after creation to get the GUID
            user = await _userManager.FindByNameAsync(user.UserName);
            if (user != null)
            {
                return Result<User>.Success(user, "User created successfully.");
            }
            else
            {
                // If user retrieval fails, return failure
                return Result<User>.Fail("Failed to retrieve user.");
            }
        }
        else
        {
            var errors = string.Join("\n", result.Errors.Select(e => e.Description));
            return Result<User>.Fail(errors);
        }
    }

    public async Task<Result<User>> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null ? Result<User>.Success(user) : Result<User>.Fail("User not found.");
    }

    public async Task<Result<User>> GetByUsernameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return user != null ? Result<User>.Success(user) : Result<User>.Fail("User not found.");
    }

    public async Task<Result<IEnumerable<User>>> GetAllUsersAsync()
    {
        var users = _userManager.Users;
        return Result<IEnumerable<User>>.Success(users);
    }

    public async Task<Result<User>> UpdateUserAsync(User user)
    {
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            // Retrieve the user after creation to get the GUID
            user = await _userManager.FindByNameAsync(user.UserName);
            if (user != null)
            {
                return Result<User>.Success(user, "User updated successfully.");
            }
            else
            {
                return Result<User>.Fail("Failed to retrieve user.");
            }
        }
        else
        {
            var errors = string.Join("\n", result.Errors.Select(e => e.Description));
            return Result<User>.Fail(errors);
        }
    }

    public async Task<Result<bool>> DeleteUserAsync(User user)
    {
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return Result<bool>.Success(true, "User deleted successfully.");
        }
        else
        {
            var errors = string.Join("\n", result.Errors.Select(e => e.Description));
            return Result<bool>.Fail(errors);
        }

    }
}