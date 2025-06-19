using System;

namespace FirebaseControl.Models
{
    public class CustomAdModel
    {
        public string Id { get; set; }
        public string Type { get; set; } = "custom";

        public string Title { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public string ButtonUrl { get; set; }
        public string ButtonText { get; set; }
        public bool ShowButton { get; set; }

        public string Day { get; set; }             // формат "yyyy-MM-dd"
        public int Priority { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
