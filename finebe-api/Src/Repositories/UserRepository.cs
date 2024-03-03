namespace finebe_api.Repositories;
using finebe_api.Interfaces;

public class UserRepository : IUserRepository
{
    private readonly DbContext _context;

    public UserRepository(DbContext context)
    {
        _context = context;
    }
}
