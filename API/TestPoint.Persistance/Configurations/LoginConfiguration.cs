using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal class LoginConfiguration : IEntityTypeConfiguration<SystemLogin>
{
    public void Configure(EntityTypeBuilder<SystemLogin> builder)
    {
        builder.ToTable("Login");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("LoginId");
        builder.Property(x => x.LoginType).HasColumnName("LoginType");
        builder.Property(x => x.Username).HasColumnName("Username");
        builder.Property(x => x.PasswordHash).HasColumnName("PasswordHash");
        builder.Property(x => x.PasswordReseted).HasColumnName("PasswordReseted");
        builder.Property(x => x.RegistryDate).HasColumnName("RegistryDate");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
    }
}