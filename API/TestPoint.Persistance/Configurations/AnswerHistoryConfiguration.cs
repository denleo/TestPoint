using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestPoint.Domain;

namespace TestPoint.DAL.Configurations;

internal sealed class AnswerHistoryConfiguration : IEntityTypeConfiguration<AnswerHistory>
{
    public void Configure(EntityTypeBuilder<AnswerHistory> builder)
    {
        builder.ToTable("AnswerHistory");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("AnswerHistoryId").IsRequired();
        builder.Property(x => x.AnswerText).HasColumnName("AnswerText").HasMaxLength(1000).IsRequired();

        builder
           .HasOne<Question>()
           .WithMany()
           .HasForeignKey(x => x.QuestionId)
           .OnDelete(DeleteBehavior.NoAction)
           .IsRequired();
    }
}