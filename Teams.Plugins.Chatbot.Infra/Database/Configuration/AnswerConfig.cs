using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Plugins.Chatbot.Core.Models;

namespace Teams.Plugins.Chatbot.Infra.Database.Configuration
{
    public class AnswerConfig : BaseEntityConfig<Answer>
    {
        public override void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            builder
                .ToTable("Answer");
            builder
                .HasIndex(x => x.Text)
                .IsUnique();
            builder
                .HasOne(x => x.Question)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.QuestionId);

            base.Configure(builder);
        }
    }
}
