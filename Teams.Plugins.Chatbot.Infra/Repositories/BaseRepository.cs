using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.Plugins.Chatbot.Core.Models;
using Teams.Plugins.Chatbot.Core.Repositories;
using Teams.Plugins.Chatbot.Infra.Database;

namespace Teams.Plugins.Chatbot.Infra.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly ChatbotDbContext _dbContext;

        protected BaseRepository(ChatbotDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<TEntity> AddAsync(TEntity item)
        {
            _dbContext
                .Set<TEntity>()
                .Add(item);

            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);

            return item;
        }

        public async Task DeleteAsync(TEntity item)
        {
            var entity = await _dbContext
                .Set<TEntity>()
                .FindAsync(item.Id);

            _dbContext
                .Set<TEntity>()
                .Remove(entity);

            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext
                .Set<TEntity>()
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public async Task UpdateAsync(TEntity item)
        {
            var entity = await _dbContext
                .Set<TEntity>()
                .FindAsync(item.Id);

            _dbContext
                .Entry(entity)
                .CurrentValues
                .SetValues(item);

            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
