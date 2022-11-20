using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal class AdminConfiguration : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.ToTable("Administrator");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("AdministratorId");
        builder.Property(x => x.LoginId).HasColumnName("LoginId");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(x => x.Login).WithOne().HasForeignKey<Administrator>(x => x.LoginId);
    }
}