using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal sealed class AdminConfiguration : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.ToTable("Administrator");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("AdministratorId").IsRequired();

        builder.HasOne(x => x.Login).WithOne().HasForeignKey<Administrator>("LoginId"); //shadow prop
    }
}