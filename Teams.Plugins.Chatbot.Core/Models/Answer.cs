namespace Teams.Plugins.Chatbot.Core.Models
{
    public class Answer : BaseEntity
    {
        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public string Text { get; set; }
    }
}
