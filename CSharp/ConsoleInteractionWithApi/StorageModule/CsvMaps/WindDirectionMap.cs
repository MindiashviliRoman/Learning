using ConsoleInteractionWithApi.DataModule;
using CsvHelper.Configuration;

namespace ConsoleInteractionWithApi.StorageModule.CsvMaps
{
    internal class WindDirectionMap : ClassMap<WindDirection>
    {
        public WindDirectionMap()
        {
            Map(m => m.Unit).Name("WindDirectionUnit");
            Map(m => m.Value).Name("WindDirectionValue");
        }
    }
}
