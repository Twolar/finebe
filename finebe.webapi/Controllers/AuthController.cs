﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using finebe.entities.Identity;
using finebe.entities.Login;
using finebe.entities.ResetPassword;
using finebe.entities.Settings;
using finebe.infrastructure.Messaging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace finebe.webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IOptions<AuthSettings> _authSettings;
    private readonly IEmailService _emailService;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IOptions<AuthSettings> authSettings,
        IEmailService emailService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _authSettings = authSettings;
        _emailService = emailService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Unauthorized();
        }

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id),
        };

        var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("JWT secret key is not set in the environment variables.");
        }
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
    }

    [HttpPost("request-password-reset")]
    public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordRequestModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            // User doesn't exist, return a generic response to avoid enumeration attacks
            return Ok("If an account matches that email, a password reset email has been sent.");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var resetLink = $"{_authSettings.Value.ResetPasswordUrl}/{token}"; var message = $"To reset your password, please click on this link {resetLink}";

        await _emailService.SendEmailAsync(model.Email, "Reset Password", message);

        return Ok("Password reset email has been sent.");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return BadRequest("Invalid token or email.");

        var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        if (!resetPassResult.Succeeded)
            return BadRequest(resetPassResult.Errors);

        return Ok("Password has been successfully reset.");
    }

    [HttpPost("validate-reset-token")]
    public async Task<IActionResult> ValidateResetToken(ValidateResetTokenModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return BadRequest("Invalid token or email.");

        var validToken = await _userManager.VerifyUserTokenAsync(user,
                            _userManager.Options.Tokens.PasswordResetTokenProvider,
                            "ResetPassword", model.Token);

        if (!validToken)
            return BadRequest("Invalid token.");

        return Ok("Token is valid.");
    }
}
