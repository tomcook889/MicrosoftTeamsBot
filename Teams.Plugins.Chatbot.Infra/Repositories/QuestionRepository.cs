using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Teams.Plugins.Chatbot.Core.Models;
using Teams.Plugins.Chatbot.Core.Repositories;
using Teams.Plugins.Chatbot.Infra.Database;

namespace Teams.Plugins.Chatbot.Infra.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ChatbotDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Question> GetAsync(string text)
        {
            return await _dbContext.Questions
                .Where(x => string.Equals(x.Text, text, StringComparison.OrdinalIgnoreCase))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task AsnwerQuestion(int userId, int questionId, int answerId)
        {
            _dbContext.Add(new AnsweredQuestion(userId, questionId, answerId));

            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
