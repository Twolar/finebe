using finebe.webapi.Src.Interfaces;
using finebe.webapi.Src.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace finebe.webapi.Src.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IAuthenticatedUserService _authenticatedUser;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUserService)
    : base(options)
    {
        _authenticatedUser = authenticatedUserService;
    }

    public DbSet<TripModel> Trips { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        if (!string.IsNullOrEmpty(_authenticatedUser.Uid))
        {
            var currentUserId = Guid.Parse(_authenticatedUser.Uid); // Assumes GUID is used for user IDs

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = currentUserId;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedAt = DateTime.Now;
                    entry.Entity.LastModifiedBy = currentUserId;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);

        }
        else 
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedAt = DateTime.Now;
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}