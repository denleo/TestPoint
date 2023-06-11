using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

public class UserGoogleAccountMappingConfiguration : IEntityTypeConfiguration<UserGoogleAccountMapping>
{
    public void Configure(EntityTypeBuilder<UserGoogleAccountMapping> builder)
    {
        builder.ToTable("UserGoogleAccountMapping");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("UserGoogleAccountMappingId").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(x => x.GoogleSub).HasColumnName("GoogleSub").IsRequired(false);

        builder.HasIndex(x => new { x.UserId, x.GoogleSub }).IsUnique().HasDatabaseName("UQ_UserGoogleAccountMapping_UserId_GoogleSub");
    }
}
