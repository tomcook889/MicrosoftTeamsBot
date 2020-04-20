using System;

namespace Teams.Plugins.Chatbot.Core.Models
{
    public class WorkingStatus : BaseEntity
    {
        public WorkingStatus(int userId)
        {
            if (userId == default) { throw new ArgumentException(nameof(userId)); }

            UserId = userId;
        }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }

    }
}
