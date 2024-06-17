using finebe.webapi.Src.Interfaces;
using finebe.webapi.Src.Models.Generic;
using finebe.webapi.Src.Models.Login;
using finebe.webapi.Src.Models.ResetPassword;
using Microsoft.AspNetCore.Mvc;

namespace finebe.webapi.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            _logger.LogInformation("RequestHeadersv2: {RequestHeaders}", HttpContext.Request.Headers);
            _logger.LogInformation("ResponseHeadersv2: {ResponseHeaders}", HttpContext.Response.Headers);

            try
            {
                if (ModelState.IsValid)
                {
                    var authToken = await _authService.LoginAsync(model);
                    _logger.LogInformation($"AuthToken: {authToken}");

                    if (authToken == null)
                        return Unauthorized();


                    return Ok(authToken);
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newAuthToken = await _authService.RefreshTokenAsync(model.Token, model.RefreshToken);
                    if (newAuthToken == null)
                        return Unauthorized();

                    return Ok(newAuthToken);
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("RequestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordActionRequestDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.RequestPasswordResetAsync(model);

                    if (result)
                    {
                        return Ok("Password reset email has been sent.");
                    }
                    else
                    {
                        return Ok("If an account matches that email, a password reset email has been sent.");
                    }
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.ResetPasswordAsync(model);

                    if (result)
                    {
                        return Ok("Password has been successfully reset.");
                    }
                    else
                    {
                        return BadRequest("Failed to reset password.");
                    }
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("ValidateResetToken")]
        public async Task<IActionResult> ValidateResetToken([FromBody] ValidateResetTokenRequestDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _authService.ValidateResetTokenAsync(model);

                    if (result)
                    {
                        return Ok("Token is valid.");
                    }
                    else
                    {
                        return BadRequest("Invalid token or email.");
                    }
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
