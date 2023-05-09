using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal sealed class TestAssignmentConfiguration : IEntityTypeConfiguration<TestAssignment>
{
    public void Configure(EntityTypeBuilder<TestAssignment> builder)
    {
        builder.ToTable("TestAssignment");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("TestAssignmentId").IsRequired();
        builder.Property(x => x.TestId).HasColumnName("TestId").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("UserId").IsRequired();

        builder
            .HasOne(x => x.Test)
            .WithMany()
            .HasForeignKey(x => x.TestId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder
            .HasOne(x => x.TestCompletion)
            .WithOne()
            .HasForeignKey<TestCompletion>(x => x.TestAssignmentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasIndex("TestId", "UserId").IsUnique().HasDatabaseName("UQ_TestAssignment_TestId_UserId");
    }
}
