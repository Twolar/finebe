using finebe.webapi.Src.Persistence.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace finebe.webapi;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
       // Ensuring ApplicationId is required
        builder.Property(t => t.ApplicationUserId)
            .IsRequired();

        // Relationships
        builder.HasOne(t => t.ApplicationUser)
            .WithMany(u => u.Trips)
            .HasForeignKey(t => t.ApplicationUserId);
    }
}
