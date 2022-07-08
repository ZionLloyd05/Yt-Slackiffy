using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slackiffy.Models
{
    [Table("Slackiffy_Users")]
    public class User
    {
        public User()
        {
            ChatMessagesFromUsers = new HashSet<Message>();
            ChatMessagesToUsers = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public DateTime DateJoined { get; set; }
        public virtual ICollection<Message> ChatMessagesFromUsers { get; set; }
        public virtual ICollection<Message> ChatMessagesToUsers { get; set; } = new HashSet<Message>();
    }
}
