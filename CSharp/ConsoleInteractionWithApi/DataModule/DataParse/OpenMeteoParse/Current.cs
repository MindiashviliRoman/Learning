using System.Text.Json.Serialization;

namespace ConsoleInteractionWithApi.DataModule.DataParse.OpenMeteoParse
{
    internal class Current
    {
        [JsonInclude]
        public string time;
        [JsonInclude]
        public int interval;
        [JsonInclude]
        public float temperature_2m;
        [JsonInclude]
        public float wind_speed_10m;
        [JsonInclude] 
        public float wind_direction_10m;
        [JsonInclude] 
        public float relative_humidity_2m;
    }
}
