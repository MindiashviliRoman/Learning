using ConsoleInteractionWithApi.DataModule;
using CsvHelper.Configuration;

namespace ConsoleInteractionWithApi.StorageModule.CsvMaps
{
    internal class WindSpeedMap : ClassMap<WindSpeed>
    {
        public WindSpeedMap()
        {
            Map(m => m.Unit).Name("WindSpeedUnit");
            Map(m => m.Value).Name("WindSpeedValue");
        }
    }
}
