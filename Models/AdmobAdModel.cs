using System;

namespace FirebaseControl.Models
{
    public class AdmobAdModel
    {
        public string Id { get; set; }
        public string Type { get; set; } = "admob";

        public string AdFormat { get; set; }        // Например: "native"
        public string AdUnitId { get; set; }
        public bool Show { get; set; }

        public string Day { get; set; }
        public int Priority { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
