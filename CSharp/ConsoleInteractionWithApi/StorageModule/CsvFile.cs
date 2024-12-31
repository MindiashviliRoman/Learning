using ConsoleInteractionWithApi.DataModule;
using ConsoleInteractionWithApi.StorageModule.CsvMaps;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace ConsoleInteractionWithApi.StorageModule
{
    internal class CsvFile
    {
        private const string DataDelimiter = "\t";
        public List<BaseWeatherData> Read(string fileFullPath, int countLines)
        {
            using (var reader = new StreamReader(fileFullPath))
            {
                CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture);
                
                config.Delimiter = DataDelimiter;
                config.Encoding = Encoding.UTF8;
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<BaseWeatherDataMap>();
                    var records = csv.GetRecords<BaseWeatherData>();
                    //records = records.Reverse(); //TODO: Add optional reverse And research reverse for lazy calculating and without coping 
                    if (records != null)
                    {
                        var readingRowCount = 2;
                        if(countLines != -1)
                        {
                            readingRowCount = countLines;
                        }
                        var result = new List<BaseWeatherData>(readingRowCount);
                        var readedLine = 0;
                        foreach (var record in records)
                        {
                            result.Add(record);
                            ++readedLine;
                            if(readedLine == countLines)
                            {
                                break;
                            }
                        }
                        return result;
                    }
                }
            }
            return null;
        }

        public void Write(string fileFullPath, List<BaseWeatherData> data)
        {
            var append = true; //TODO: optional append write to csv
            using (var writer = new StreamWriter(fileFullPath, append))
            {
                CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture);
                config.Delimiter = DataDelimiter;
                //config.Encoding = Encoding.UTF8;
                if (append)
                {
                    config.HasHeaderRecord = !File.Exists(fileFullPath) || new FileInfo(fileFullPath).Length == 0;
                }
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.Context.RegisterClassMap<BaseWeatherDataMap>();
                    csv.WriteRecords(data);
                }
            }
        }
    }
}
