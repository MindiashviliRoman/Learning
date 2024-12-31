namespace ConsoleInteractionWithApi.DataModule
{
    internal class BaseWeatherData
    {
        public DateTime ForecastDateTime { get; set; }
        public TemperatureData Temperature { get; set; }
        public WindData Wind { get; set; }
        public RelativeHumidityData RelativeHumidity { get; set; }
        
        //TODO: For CsvHelper need public set property and constuctor without parameters
        public BaseWeatherData()
        { }

        public BaseWeatherData(DateTime dateTime, TemperatureData temperature, WindData wind, RelativeHumidityData humidity)
        {
            ForecastDateTime = dateTime;
            Temperature = temperature;
            Wind = wind;
            RelativeHumidity = humidity;
        }

        public override string ToString()
        {
            return String.Format("[Weather at]: Time: {0}\r\nTemprature: {1}\r\nWindData: {2}\r\nRelativeHumidityData: {3}",
                                ForecastDateTime, Temperature, Wind, RelativeHumidity);
        }
    }
}
