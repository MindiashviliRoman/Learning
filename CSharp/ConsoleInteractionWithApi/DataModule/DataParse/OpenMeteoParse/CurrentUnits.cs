using System.Text.Json.Serialization;

namespace ConsoleInteractionWithApi.DataModule.DataParse.OpenMeteoParse
{
    internal class CurrentUnits
    {
        [JsonInclude]
        public string time;
        [JsonInclude]
        public string interval;
        [JsonInclude]
        public string temperature_2m;
        [JsonInclude]
        public string wind_speed_10m;
        [JsonInclude]
        public string wind_direction_10m;
        [JsonInclude]
        public string humidity_10m;
    }
}
