using ConsoleInteractionWithApi.DataModule;
using CsvHelper.Configuration;

namespace ConsoleInteractionWithApi.StorageModule.CsvMaps
{
    internal class RelativeHumidityDataMap : ClassMap<RelativeHumidityData>
    {
        public RelativeHumidityDataMap()
        {
            Map(m => m.Unit).Name("RelativeHumidityUnit");
            Map(m => m.Value).Name("RelativeHumidityValue");
        }
    }
}
