using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Plugins.Chatbot.Core.Models;

namespace Teams.Plugins.Chatbot.Infra.Database.Configuration
{
    public class WorkingStatusConfig : BaseEntityConfig<WorkingStatus>
    {
        public override void Configure(EntityTypeBuilder<WorkingStatus> builder)
        {
            builder
                .ToTable("WorkingStatus");
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.WorkingStatuses)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.UserId);

            base.Configure(builder);
        }
    }
}
