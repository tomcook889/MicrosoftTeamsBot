using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace Teams.Plugins.Chatbot.Infra.Database
{
    public class ChatbotDbContextFactory : IDesignTimeDbContextFactory<ChatbotDbContext>
    {
        public ChatbotDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ChatbotDbContext>();
            builder.UseSqlServer(config.GetConnectionString("DefaultConnectionString"));

            return new ChatbotDbContext(builder.Options);
        }
    }
}
