using System.Collections.Generic;

namespace Teams.Plugins.Chatbot.Core.Models
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
