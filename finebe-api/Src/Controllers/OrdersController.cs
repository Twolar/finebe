using finebe_api.Models;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Formatter;

namespace finebe_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Order> Get()
        {
            return _context.Orders.Include(o => o.User);
        }

        [HttpGet("{key}")]  
        [EnableQuery]
        public SingleResult<Order> Get([FromODataUri] Guid key)
        {
            return SingleResult.Create(_context.Orders.Where(order => order.Id == key));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Created(order);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] Guid key, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Orders.Any(o => o.Id == key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var order = await _context.Orders.FindAsync(key);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
