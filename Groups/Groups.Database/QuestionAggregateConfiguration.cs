using Groups.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Groups.Database
{
    public class QuestionAggregateConfiguration : IEntityTypeConfiguration<QuestionAggregate>
    {
        public void Configure(EntityTypeBuilder<QuestionAggregate> builder)
        {
            builder.HasKey(q => q.Id);
            builder.OwnsOne(q => q.Photo).Property(photo => photo.Image).HasColumnName("Photo");
            builder.OwnsMany(q => q.Answers, answers =>
            {
                answers.ToTable("Answer");
                answers.WithOwner().HasForeignKey("QuestionId");
                answers.HasKey("QuestionId", "Value", "Correct");
            });
        }
    }
}