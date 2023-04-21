using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal sealed class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.ToTable("UserGroup");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("UserGroupId").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(256).IsRequired();
        builder.HasIndex(x => new { x.Name, x.AdministratorId }).IsUnique().HasDatabaseName("UQ_UserGroup_Name_AdministratorId");

        builder.HasOne<Administrator>().WithMany().HasForeignKey(x => x.AdministratorId);
        builder
            .HasMany(x => x.Users)
            .WithMany(x => x.UserGroups)
            .UsingEntity<Dictionary<string, object>>(
                "UserUserGroupBridge",
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.NoAction),
                j => j.HasOne<UserGroup>().WithMany().HasForeignKey("UserGroupId").OnDelete(DeleteBehavior.Cascade)
            );
    }
}
