using ConsoleInteractionWithApi.DataModule;
using CsvHelper.Configuration;

namespace ConsoleInteractionWithApi.StorageModule.CsvMaps
{
    internal class WindDataMap : ClassMap<WindData>
    {
        public WindDataMap()
        {
            References<WindSpeedMap>(m => m.Speed);
            References<WindDirectionMap>(m => m.Direction);
        }
    }
}
