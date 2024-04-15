using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace finebe_api.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

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
            SeedInitialCustomerOrderData(modelBuilder);
        }

        private void SeedInitialCustomerOrderData(ModelBuilder modelBuilder)
        {
            // Seed application users
            var applicationUsers = new List<ApplicationUser>
            {
                new ApplicationUser { Id = Guid.NewGuid(), UserName = "user1@example.com", Email = "user1@example.com" },
                new ApplicationUser { Id = Guid.NewGuid(), UserName = "user2@example.com", Email = "user2@example.com" },
                new ApplicationUser { Id = Guid.NewGuid(), UserName = "user3@example.com", Email = "user3@example.com" }
            };

            modelBuilder.Entity<ApplicationUser>().HasData(applicationUsers);

            // Seed orders
            var orders = new List<Order>();
            foreach (var user in applicationUsers)
            {
                for (int i = 1; i <= 2; i++)
                {
                    var order = new Order
                    {
                        Id = Guid.NewGuid(),
                        Amount = new Random().Next(1, 9) * 10,
                        UserId = user.Id
                    };
                    orders.Add(order);
                }
            }

            modelBuilder.Entity<Order>().HasData(orders);
        }
    }
}