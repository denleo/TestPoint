using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal sealed class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable("Answer");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("AnswerId").IsRequired();
        builder.Property(x => x.AnswerText).HasColumnName("AnswerText").HasMaxLength(1000).IsRequired();
        builder.Property(x => x.IsCorrect).HasColumnName("IsCorrect").IsRequired();

        builder
            .HasOne<Question>()
            .WithMany(x => x.Answers)
            .HasForeignKey("QuestionId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
