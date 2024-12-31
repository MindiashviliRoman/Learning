namespace ConsoleInteractionWithApi.DataModule.DataParse.OpenMeteoParse
{
    //https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=23.43&hourly=temperature_2m,wind_speed_10m&start_date=2024-11-15&end_date=2024-11-16&time_mode=time_interval

    internal class OpenMeteoCurrentStruct
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float generationtime_ms { get; set; }
        public float utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public float elevation { get; set; }
        public CurrentUnits current_units { get; set; }
        public Current current { get; set; }
    }
}
