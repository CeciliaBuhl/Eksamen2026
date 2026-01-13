using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eksamen2026.Configuration
{
    public class ConfigManager
    {
        public void SaveConfig(string filePath, VentilatorConfig config)//Gem JSON-konfigurationsfil med serialize
        {
            string json = JsonSerializer.Serialize(config);
            File.WriteAllText(filePath, json);
        }
        public VentilatorConfig LoadConfig(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath); // Læs JSON-konfigurationsfilen med deserialize
            return JsonSerializer.Deserialize<VentilatorConfig>(json) ?? new VentilatorConfig();
            }
            catch(FileNotFoundException)
            {
                return new VentilatorConfig();
            }
        }
    }
}