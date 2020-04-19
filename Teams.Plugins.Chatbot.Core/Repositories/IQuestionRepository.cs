using System.Threading.Tasks;
using Teams.Plugins.Chatbot.Core.Models;

namespace Teams.Plugins.Chatbot.Core.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<Question> GetAsync(string text);
    }
}
