using ConsoleInteractionWithApi.DataModule;
using CsvHelper.Configuration;

namespace ConsoleInteractionWithApi.StorageModule.CsvMaps
{
    internal class TemperatureDataMap : ClassMap<TemperatureData>
    {
        public TemperatureDataMap()
        {
            Map(m => m.Unit).Name("TemperatureUnit");
            Map(m => m.Value).Name("TemperatureValue");
        }
    }
}
