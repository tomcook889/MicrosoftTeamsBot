using System.Threading.Tasks;
using Teams.Plugins.Chatbot.Core.Models;

namespace Teams.Plugins.Chatbot.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindAsync(string userPrincipalName);
    }
}
