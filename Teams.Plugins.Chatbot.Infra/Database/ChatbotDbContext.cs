using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Teams.Plugins.Chatbot.Core.Models;
using Teams.Plugins.Chatbot.Infra.Database.Configuration;

namespace Teams.Plugins.Chatbot.Infra.Database
{
    public class ChatbotDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<WorkingStatus> WorkingStatuses { get; set; }

        public ChatbotDbContext(DbContextOptions<ChatbotDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new WorkingStatusConfig());
        }

        public override int SaveChanges()
        {
            UpdateBaseEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateBaseEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateBaseEntities()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created = DateTime.Now;
                }

                entry.Entity.Updated = DateTime.Now;
            }
        }
    }

}
