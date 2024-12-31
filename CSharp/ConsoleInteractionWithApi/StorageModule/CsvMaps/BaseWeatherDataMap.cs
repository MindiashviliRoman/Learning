using ConsoleInteractionWithApi.DataModule;
using CsvHelper.Configuration;

namespace ConsoleInteractionWithApi.StorageModule.CsvMaps
{
    internal class BaseWeatherDataMap : ClassMap<BaseWeatherData>
    {
        public BaseWeatherDataMap()
        {
            Map(m => m.ForecastDateTime);
            References<TemperatureDataMap>(m => m.Temperature);
            References<WindDataMap>(m => m.Wind);
            References<RelativeHumidityDataMap>(m => m.RelativeHumidity);
        }
    }
}
