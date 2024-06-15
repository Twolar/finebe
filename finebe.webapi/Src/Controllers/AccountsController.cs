using finebe.webapi.Src.Models.Register;
using finebe.webapi.Src.Persistence.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finebe.webapi.Src.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequestDto model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Avatar = model.Avatar
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new { Message = "Registration successful" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(ModelState);
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    [HttpPost("UploadAvatar")]
    public async Task<IActionResult> UploadAvatar(IFormFile avatar)
    {
        try
        {
            if (avatar == null || avatar.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolder = Path.Combine("wwwroot", "Avatars");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + avatar.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream);
            }

            var avatarUrl = Url.Content($"~/Avatars/{uniqueFileName}");

            return Ok(new { avatarUrl });
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
