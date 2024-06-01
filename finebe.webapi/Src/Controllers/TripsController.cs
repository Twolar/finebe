using finebe.webapi.Src.Interfaces;
using finebe.webapi.Src.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace finebe.webapi.Src.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public TripsController(ApplicationDbContext context, IAuthenticatedUserService authenticatedUserService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(TripModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                // Set the current user's ID
                var currentUserId = Guid.Parse(_authenticatedUserService.Uid);

                _context.Trips.Add(model);
                await _context.SaveChangesAsync();

                return Ok("Created");
            }

            return BadRequest(ModelState);
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}
