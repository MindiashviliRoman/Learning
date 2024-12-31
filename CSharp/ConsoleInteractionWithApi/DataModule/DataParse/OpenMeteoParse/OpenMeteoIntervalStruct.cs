namespace ConsoleInteractionWithApi.DataModule.DataParse.OpenMeteoParse
{
    internal class OpenMeteoIntervalStruct
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float generationtime_ms { get; set; }
        public float utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public float elevation { get; set; }
        public CurrentUnits hourly_units { get; set; }
        public Hourly hourly { get; set; }
    }
}
