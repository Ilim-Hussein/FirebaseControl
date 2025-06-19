using System.Text.Json.Serialization;

namespace FirebaseControl.Models
{
    public class ConfigModel
    {

        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; }

        [JsonPropertyName("credentialsPath")]
        public string CredentialsPath { get; set; }
    }
}
