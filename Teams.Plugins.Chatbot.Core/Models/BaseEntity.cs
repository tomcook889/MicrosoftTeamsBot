using System;

namespace Teams.Plugins.Chatbot.Core.Models
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
