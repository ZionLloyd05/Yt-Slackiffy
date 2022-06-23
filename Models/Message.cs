using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slackiffy.Models
{
    [Table("Slackiffy_Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime When { get; set; }
        public int ParentMessageId { get; set; }
        public ICollection<Message> Responses { get; set; }
        public int UserId { get; set; }
        public int RecieverId { get; set; }
    }
}
