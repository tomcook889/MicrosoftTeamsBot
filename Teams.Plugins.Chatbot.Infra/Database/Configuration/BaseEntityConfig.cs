using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Plugins.Chatbot.Core.Models;

namespace Teams.Plugins.Chatbot.Infra.Database.Configuration
{
    public abstract class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Created)
                .IsRequired();
            builder
                .Property(x => x.Updated)
                .IsRequired();
        }
    }
}
