using finebe_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace finebe_api.Controllers
{
    public class OrdersController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Order> Get()
        {
            // Assuming you have a User property in your Order entity pointing to the ApplicationUser
            // You can include ApplicationUser data if needed
            return _context.Orders.Include(o => o.User);
        }

        [EnableQuery]
        public ActionResult<Order> Get([FromODataUri] Guid key)
        {
            // Assuming you have a User property in your Order entity pointing to the ApplicationUser
            // You can include ApplicationUser data if needed
            var order = _context.Orders.Include(o => o.User).FirstOrDefault(o => o.Id == key);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}
