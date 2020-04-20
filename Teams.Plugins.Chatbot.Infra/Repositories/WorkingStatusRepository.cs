using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Teams.Plugins.Chatbot.Core.Models;
using Teams.Plugins.Chatbot.Core.Repositories;
using Teams.Plugins.Chatbot.Infra.Database;

namespace Teams.Plugins.Chatbot.Infra.Repositories
{
    public class WorkingStatusRepository : BaseRepository<WorkingStatus>, IWorkingStatusRepository
    {
        public WorkingStatusRepository(ChatbotDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<WorkingStatus> GetAsync(int userId, DateTime date)
        {
            return await _dbContext.WorkingStatuses
                .Where(x => x.UserId == userId && x.Date == date)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }
    }
}
