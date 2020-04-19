using System;

namespace Teams.Plugins.Chatbot.Core.Models
{
    public class AnsweredQuestion : BaseEntity
    {
        public AnsweredQuestion(int userId, int questionId, int answerId)
        {
            if (userId == default) { throw new ArgumentException(nameof(userId)); }
            if (questionId == default) { throw new ArgumentException(nameof(questionId)); }
            if (answerId == default) { throw new ArgumentException(nameof(answerId)); }

            UserId = userId;
            QuestionId = questionId;
            AnswerId = answerId;
        }

        public int UserId { get; set; }

        public User User { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public int AnswerId { get; set; }

        public Answer Answer { get; set; }
    }
}
