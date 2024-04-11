using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace finebe_api.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    // Add DbSets for your entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed default roles
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" }
            );

            // Load password from configuration
            var defaultUserPassword = Environment.GetEnvironmentVariable("DEFAULT_USER_PASSWORD");

            // TODO: FromProd: Remove me later...
            // Seed default user
            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "taylor@finebe.com",
                    NormalizedUserName = "TAYLOR@FINEBE.COM",
                    Email = "taylor@finebe.com",
                    NormalizedEmail = "TAYLOR@FINEBE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, defaultUserPassword)
                }
            );

            // Configure your custom entity seeding if any
        }
}

