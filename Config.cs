using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;


namespace csgo2_demo_s3
{
    public class S3Config: BasePluginConfig

    {
        [JsonPropertyName("AccessKey")] public string AccessKey { get; set; } = string.Empty;

        [JsonPropertyName("SecretKey")] public string SecretKey { get; set; } = string.Empty;

        [JsonPropertyName("EndPoint")] public string EndPoint { get; set; } = string.Empty;
        [JsonPropertyName("DemoStorePath")] public string DemoStorePath { get; set; } = string.Empty;
    }
}
