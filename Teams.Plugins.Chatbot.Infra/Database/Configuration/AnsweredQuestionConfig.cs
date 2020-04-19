using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Plugins.Chatbot.Core.Models;

namespace Teams.Plugins.Chatbot.Infra.Database.Configuration
{
    public class AnsweredQuestionConfig : BaseEntityConfig<AnsweredQuestion>
    {
        public override void Configure(EntityTypeBuilder<AnsweredQuestion> builder)
        {
            builder
                .ToTable("AnsweredQuestion");
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.AnsweredQuestions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.UserId);
            builder
                .HasOne(x => x.Question)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.QuestionId);
            builder
                .HasOne(x => x.Answer)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.AnswerId);

            base.Configure(builder);
        }
    }
}
