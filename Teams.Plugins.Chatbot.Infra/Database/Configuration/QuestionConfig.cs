using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Plugins.Chatbot.Core.Models;

namespace Teams.Plugins.Chatbot.Infra.Database.Configuration
{
    public class QuestionConfig : BaseEntityConfig<Question>
    {
        public override void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();
            builder
                .ToTable("Question");
            builder
                .HasIndex(x => x.Text)
                .IsUnique();
            
            base.Configure(builder);
        }
    }
}
