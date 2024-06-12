using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using finebe.webapi.Src.Helpers;
using finebe.webapi.Src.Interfaces;
using finebe.webapi.Src.Models.Login;
using finebe.webapi.Src.Models.ResetPassword;
using finebe.webapi.Src.Models.Settings;
using finebe.webapi.Src.Persistence.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace finebe.webapi.Src.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // TODO TLB: Refresh tokens?

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOptions<AuthSettings> _authSettings;
    private readonly IEmailService _emailService;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        IOptions<AuthSettings> authSettings,
        IEmailService emailService)
    {
        _userManager = userManager;
        _authSettings = authSettings;
        _emailService = emailService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        try
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

            var claimsIdentity = new ClaimsIdentity(authClaims, "Bearer");

            var secretKey = EnvVariableHelper.GetByKey("JWT_SECRET");
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                expiration = token.ValidTo,
            });
        }
        catch (Exception ex)
        {
            // Log the exception (consider using a logging framework)
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("request-password-reset")]
    public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordActionRequestDto model)
    {
        try
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
        catch (System.Exception)
        {

            throw;
        }
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto model)
    {
        try
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
        catch (System.Exception)
        {
            throw;
        }
    }

    [HttpPost("validate-reset-token")]
    public async Task<IActionResult> ValidateResetToken(ValidateResetTokenRequestDto model)
    {
        try
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
        catch (System.Exception)
        {

            throw;
        }
    }
}
