using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using FirebaseControl.Interfaces;


namespace FirebaseControl.Models
{
    public class UserMapping : IUserMapper
    {
        private const string MappingFilePath = "Resources/user_map.json";

        [JsonPropertyName("Map")]
        public Dictionary<string, string> Map { get; set; } = new Dictionary<string, string>();

        public string GetReadableId(string firebaseUid)
        {
            if (!Map.ContainsKey(firebaseUid))
            {
                int nextId = Map.Count + 1;
                Map[firebaseUid] = $"user_{nextId:D3}";
            }

            return Map[firebaseUid];
        }

        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(MappingFilePath, JsonSerializer.Serialize(this, options));
        }

        public void SaveToFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            File.WriteAllText(MappingFilePath, JsonSerializer.Serialize(this, options));
        }

        public static UserMapping LoadFromFile()
        {
            if (!File.Exists(MappingFilePath))
                return new UserMapping();

            var json = File.ReadAllText(MappingFilePath);
            return JsonSerializer.Deserialize<UserMapping>(json) ?? new UserMapping();
        }
    }
}
