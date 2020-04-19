using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Teams.Plugins.Chatbot.Core.Models;
using Teams.Plugins.Chatbot.Core.Repositories;
using Teams.Plugins.Chatbot.Infra.Database;

namespace Teams.Plugins.Chatbot.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ChatbotDbContext dbContext)
            : base (dbContext)
        {
        }

        public async Task<User> FindAsync(string userPrincipalName)
        {
            return await _dbContext.Users
                .Where(x => string.Equals(x.UserPrincipalName, userPrincipalName, System.StringComparison.OrdinalIgnoreCase))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }
    }
}
