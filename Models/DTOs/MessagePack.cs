using System;

namespace Slackiffy.Models.DTOs
{
    public class MessagePack
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
