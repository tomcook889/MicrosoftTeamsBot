using System;
using System.Threading.Tasks;
using Teams.Plugins.Chatbot.Core.Models;

namespace Teams.Plugins.Chatbot.Core.Repositories
{
    public interface IWorkingStatusRepository : IRepository<WorkingStatus>
    {
        Task<WorkingStatus> GetAsync(int userId, DateTime date);
    }
}
