using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Eksamen2026.Configuration
{
    public class VentilatorConfig
    {
        [JsonPropertyName("Off-setting")]
        public int OffSetting { get; set; } = 800;//standardværdier

        [JsonPropertyName("Low-setting")]
        public int LowSetting { get; set; } = 1000;

        [JsonPropertyName("Medium-setting")]
        public int MediumSetting { get; set; } = 1500;

        [JsonPropertyName("High-setting")]
        public int HighSetting { get; set; } = 2000;
    }
}