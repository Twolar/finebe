using finebe_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace finebe_api.Controllers;

public class UsersController : ODataController
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [EnableQuery]
    public IQueryable<ApplicationUser> Get()
    {
        return _userManager.Users;
    }
}