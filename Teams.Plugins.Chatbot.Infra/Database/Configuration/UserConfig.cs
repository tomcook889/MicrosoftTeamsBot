using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Plugins.Chatbot.Core.Models;

namespace Teams.Plugins.Chatbot.Infra.Database.Configuration
{
    public class UserConfig : BaseEntityConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("User");
            builder
                .HasAlternateKey(x => x.UserPrincipalName);
            builder
                .Property(x => x.Email)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
