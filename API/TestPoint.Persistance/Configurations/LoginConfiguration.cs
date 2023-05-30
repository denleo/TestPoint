using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal sealed class LoginConfiguration : IEntityTypeConfiguration<SystemLogin>
{
    public void Configure(EntityTypeBuilder<SystemLogin> builder)
    {
        builder.ToTable("Login");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("LoginId").IsRequired();
        builder.Property(x => x.LoginType).HasColumnName("LoginType").IsRequired();
        builder.Property(x => x.Username).HasColumnName("Username").HasMaxLength(256).IsRequired();
        builder.Property(x => x.PasswordHash).HasColumnName("PasswordHash").HasMaxLength(256).IsRequired(false);
        builder.Property(x => x.PasswordReseted).HasColumnName("PasswordReseted").IsRequired();
        builder.Property(x => x.RegistryDate).HasColumnName("RegistryDate").IsRequired();

        builder.HasIndex(x => new { x.Username, x.LoginType }).IsUnique().HasDatabaseName("UQ_Login_Username_LoginType");
    }
}