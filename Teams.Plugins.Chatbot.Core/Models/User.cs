using System;
using System.Collections.Generic;

namespace Teams.Plugins.Chatbot.Core.Models
{
    public class User : BaseEntity
    {
        public User(
            string userPrincipalName,
            string email)
        {
            if (string.IsNullOrEmpty(userPrincipalName)) { throw new ArgumentNullException(nameof(userPrincipalName)); }
            if (string.IsNullOrEmpty(email)) { throw new ArgumentNullException(nameof(email)); }

            UserPrincipalName = userPrincipalName;
            Email = email;
        }

        //private User()
        //{
        //    // For EF
        //}


        public string UserPrincipalName { get; set; }

        public string Email { get; set; }
        public List<WorkingStatus> WorkingStatuses { get; set; }

    }
}
