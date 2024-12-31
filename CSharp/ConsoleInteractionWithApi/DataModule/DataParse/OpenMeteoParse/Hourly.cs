using System.Text.Json.Serialization;

namespace ConsoleInteractionWithApi.DataModule.DataParse.OpenMeteoParse
{
    internal class Hourly
    {
        public string[] time {get; set;}
        public float[] temperature_2m {get; set;}
        public float[] wind_speed_10m {get; set;}
        public float[] wind_direction_10m { get; set; }
        public float[] relative_humidity_2m {get; set;}
    }
}
