using ConsoleInteractionWithApi.DataModule;
using ConsoleInteractionWithApi.DataModule.DataParse;
using System.Globalization;

namespace ConsoleInteractionWithApi.ApiServices.OpenMeteo
{
    internal class OpenMeteoApiController : IMeteoProvider
    {
        const string baseUrl = "https://api.open-meteo.com/v1/";
        private HttpClient _client;
        private OpenMeteoDataParser _parser;

        public OpenMeteoApiController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
            _parser = new OpenMeteoDataParser();
        }

        public async Task<string> GetResponseAsync(string request)
        {
            //var result = await _client.GetStringAsync(request)
            var response = await _client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        #region IMeteoProvider
        public async Task<BaseWeatherData> GetCurrentWeatherAsync(float latitude, float longitude)
        {
            var requestUrl = $"forecast?latitude={FloatFormatToString(latitude)}&longitude={FloatFormatToString(longitude)}" +
                $"&current=" +
                $"temperature_2m," +
                $"wind_speed_10m," +
                $"wind_direction_10m," +
                $"relative_humidity_2m";

            var answer = await GetResponseAsync(requestUrl);
            return _parser.GetCurrentWeather(answer);
        }

        public async Task<List<BaseWeatherData>> GetForecastWeatherAsync(float latitude, float longitude, string dateFrom, string dateTo)
        {
            var requestUrl = $"forecast?latitude={FloatFormatToString(latitude)}&longitude={FloatFormatToString(longitude)}" +
                $"&hourly=" +
                $"temperature_2m," +
                $"wind_speed_10m," +
                $"wind_direction_10m," +
                $"relative_humidity_2m" +
                $"&start_date={dateFrom}" +
                $"&end_date={dateTo}" +
                $"&time_mode=time_interval";

            var answer = await GetResponseAsync(requestUrl);
            return _parser.GetIntervalWeather(answer);
        }
        private string FloatFormatToString(float value)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", value);
        }

        //private async static Task WriteToFileAsync(string text)
        //{
        //    string path = "E:/note1.txt";

        //    // полная перезапись файла 
        //    using (StreamWriter writer = new StreamWriter(path, false))
        //    {
        //        await writer.WriteLineAsync(text);
        //    }
        //    // добавление в файл
        //    using (StreamWriter writer = new StreamWriter(path, true))
        //    {
        //        await writer.WriteLineAsync("Addition");
        //        await writer.WriteAsync("4,5");
        //    }
        //}

#endregion

public void Dispose()
        {
            _client.Dispose();
        }

    }
}
