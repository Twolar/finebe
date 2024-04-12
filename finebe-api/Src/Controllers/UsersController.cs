using finebe_api.Models;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Formatter;

namespace finebe_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ODataController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [HttpGet]
        [EnableQuery]
        public IQueryable<ApplicationUser> Get()
        {
            return _context.Users;
        }

        [HttpGet("{key}")]  
        [EnableQuery]
        public SingleResult<ApplicationUser> Get([FromODataUri] Guid key)
        {
            return SingleResult.Create(_context.Users.Where(order => order.Id == key));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return Created(user);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] Guid key, [FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != user.Id)
            {
                return BadRequest();
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var user = await _context.Users.FindAsync(key);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
