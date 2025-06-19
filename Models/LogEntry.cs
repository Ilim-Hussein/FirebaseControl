using System;

namespace FirebaseControl.Models
{
    public class LogEntry
    {
        public string AdId { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }           // "custom" или "admob"
        public string Event { get; set; }          // "view", "click", ...
        public DateTime Timestamp { get; set; }
    }
}
