using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Question");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("QuestionId").IsRequired();
        builder.Property(x => x.QuestionText).HasColumnName("QuestionText").HasMaxLength(1000).IsRequired();
        builder.Property(x => x.QuestionType).HasColumnName("QuestionType").IsRequired();

        builder
            .HasOne<Test>()
            .WithMany(x => x.Questions)
            .HasForeignKey("TestId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
