using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace finebe_api.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
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

            // Seed default user
            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Password123$")
                }
            );

            // Configure your custom entity seeding if any
        }
}

