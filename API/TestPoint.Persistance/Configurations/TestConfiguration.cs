using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal sealed class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("Test");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("TestId").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(256).IsRequired();
        builder.Property(x => x.Difficulty).HasColumnName("Difficulty").IsRequired();
        builder.Property(x => x.EstimatedTime).HasColumnName("EstimatedTime").IsRequired();
        builder.Property(x => x.Author).HasColumnName("Author").HasMaxLength(16).IsRequired();

        builder.HasCheckConstraint("CK_Test_EstimatedTime", "EstimatedTime > 0");

        builder.HasOne<Administrator>().WithMany().HasForeignKey(x => x.AuthorId).IsRequired();
        builder.HasIndex("AuthorId", "Name").IsUnique().HasDatabaseName("UQ_Test_AuthorId_Name");
    }
}
