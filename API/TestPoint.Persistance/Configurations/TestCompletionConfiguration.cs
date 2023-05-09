using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal sealed class TestCompletionConfiguration : IEntityTypeConfiguration<TestCompletion>
{
    public void Configure(EntityTypeBuilder<TestCompletion> builder)
    {
        builder.ToTable("TestCompletion");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("TestCompletionId").IsRequired();
        builder.Property(x => x.Score).HasColumnName("Score").IsRequired();
        builder.Property(x => x.CompletionTime).HasColumnName("CompletionTime").IsRequired();

        builder.HasCheckConstraint("CK_TestCompletion_Score", "Score > 0");

        builder
            .HasMany(x => x.Answers)
            .WithOne()
            .HasForeignKey("TestCompletionId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
