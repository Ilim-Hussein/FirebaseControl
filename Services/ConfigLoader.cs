using FirebaseControl.Models;
using System.IO;
using System;
using System.Text.Json;

public static class ConfigLoader
{
    public static ConfigModel LoadConfig()
    {
        // Абсолютный путь к config.json
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "Resources", "config.json");
        var json = File.ReadAllText(jsonPath);

        var config = JsonSerializer.Deserialize<ConfigModel>(json);

        // 🛠 Преобразуем credentialsPath в абсолютный путь
        if (!Path.IsPathRooted(config.CredentialsPath))
        {
            config.CredentialsPath = Path.Combine(AppContext.BaseDirectory, config.CredentialsPath);
        }

        return config;
    }
}
