using finebe.webapi.Src.Helpers;
using finebe.webapi.Src.Interfaces;
using finebe.webapi.Src.Persistence.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace finebe.webapi.Src.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    private readonly IAuthenticatedUserService _authenticatedUser;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUserService)
        : base(options)
    {
        _authenticatedUser = authenticatedUserService;
    }

    public DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TripConfiguration());

        SeedDatabase(modelBuilder);
    }

    private void SeedDatabase(ModelBuilder modelBuilder)
    {
        // Create user
        var hasher = new PasswordHasher<ApplicationUser>();
        modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
        {
            Id = new Guid("3b1af8e0-eb6b-4b4e-8d2f-e95aa5347cd2"),
            UserName = "admin@domain.com",
            NormalizedUserName = "ADMIN",
            Email = "admin@domain.com",
            NormalizedEmail = "ADMIN@DOMAIN.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, EnvVariableHelper.GetByKey("DEFAULT_ADMIN_PASSWORD")),
            SecurityStamp = string.Empty,
            Name = "Default Admin"
        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(_authenticatedUser.Email))
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = _authenticatedUser.Email;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedAt = DateTime.Now;
                    entry.Entity.LastModifiedBy = _authenticatedUser.Email;
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
                    entry.Entity.CreatedBy = "AnonymousUser";
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedAt = DateTime.Now;
                    entry.Entity.LastModifiedBy = "AnonymousUser";
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
