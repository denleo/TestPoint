using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("UserId").IsRequired();
        builder.Property(x => x.FirstName).HasColumnName("FirstName").HasMaxLength(64).IsRequired();
        builder.Property(x => x.LastName).HasColumnName("LastName").HasMaxLength(64).IsRequired();
        builder.Property(x => x.Email).HasColumnName("Email").HasMaxLength(254).IsRequired();
        builder.Property(x => x.EmailConfirmed).HasColumnName("EmailConfirmed").IsRequired();
        builder.Property(x => x.GoogleAuthenticated).HasColumnName("GoogleAuthenticated").IsRequired();
        builder.Property(x => x.Avatar).HasColumnName("Avatar").IsRequired(false);

        builder.HasIndex(x => x.Email).IsUnique().HasDatabaseName("UQ_User_Email");

        builder.HasOne(x => x.Login).WithOne().HasForeignKey<User>("LoginId"); //shadow prop
    }
}