using System.Collections.Generic;
using System.Threading.Tasks;
using Teams.Plugins.Chatbot.Core.Models;

namespace Teams.Plugins.Chatbot.Core.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> AddAsync(TEntity item);

        Task UpdateAsync(TEntity item);

        Task DeleteAsync(TEntity item);
    }
}
