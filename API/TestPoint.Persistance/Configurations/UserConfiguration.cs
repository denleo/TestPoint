using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("UserId");
        builder.Property(x => x.Avatar).HasColumnName("Avatar").IsRequired(false);
        builder.Property(x => x.FirstName).HasColumnName("FirstName");
        builder.Property(x => x.LastName).HasColumnName("LastName");
        builder.Property(x => x.Email).HasColumnName("Email");
        builder.Property(x => x.LoginId).HasColumnName("LoginId");
        builder.HasOne(x => x.Login).WithOne().HasForeignKey<User>(x => x.LoginId);
    }
}