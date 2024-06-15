using finebe.webapi.Src.Persistence.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace finebe.webapi;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
       // Ensuring ApplicationId is required
        builder.Property(t => t.UserId)
            .IsRequired();

            // Relationships
            builder.HasOne(t => t.ApplicationUser)
                   .WithMany(u => u.RefreshTokens)
                   .HasForeignKey(t => t.UserId);
    }
}
