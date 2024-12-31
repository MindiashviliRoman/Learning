using ConsoleInteractionWithApi.DataModule.DataParse.OpenMeteoParse;
using System.Text.Json;

namespace ConsoleInteractionWithApi.DataModule.DataParse
{
    internal class OpenMeteoDataParser
    {
        public BaseWeatherData GetCurrentWeather(string data)
        {
            BaseWeatherData result = null;
            OpenMeteoCurrentStruct openMeteoCurrentStruct = null;
     
            openMeteoCurrentStruct = JsonSerializer.Deserialize<OpenMeteoCurrentStruct>(data);
            var current_units = openMeteoCurrentStruct.current_units;
            var current = openMeteoCurrentStruct.current;
            var curTime = DateTime.Parse(current.time);
            var temperature = new TemperatureData(current_units.temperature_2m, current.temperature_2m);
            var windSpeed = new WindSpeed(current_units.wind_speed_10m, current.wind_direction_10m);
            var windDirection = new WindDirection(current_units.wind_direction_10m, current.wind_direction_10m);
            var wind = new WindData(windSpeed, windDirection);
            var humidity = new RelativeHumidityData(current_units.humidity_10m, current.relative_humidity_2m);
            result = new BaseWeatherData(curTime, temperature, wind, humidity);

            return result;
        }

        public List<BaseWeatherData> GetIntervalWeather(string data)
        {
            List<BaseWeatherData> result = new List<BaseWeatherData>();
            OpenMeteoIntervalStruct openMeteoIntervalStruct = null;

            openMeteoIntervalStruct = JsonSerializer.Deserialize<OpenMeteoIntervalStruct>(data);
            var hourly_units = openMeteoIntervalStruct.hourly_units;
            var hourly = openMeteoIntervalStruct.hourly;

            var count = hourly.time.Length;

            if (hourly.temperature_2m.Length != count
                || hourly.relative_humidity_2m.Length != count
                || hourly.wind_speed_10m.Length != count
                || hourly.wind_direction_10m.Length != count)
            {
                throw new Exception("Получены некорректные данные. Не возможно спарсить");
            }

            result.Capacity = count;
            for (var i = 0; i < count; ++i)
            {
                var curTime = DateTime.Parse(hourly.time[i]);
                var temperature = new TemperatureData(hourly_units.temperature_2m, hourly.temperature_2m[i]);
                var windSpeed = new WindSpeed(hourly_units.wind_speed_10m, hourly.wind_direction_10m[i]);
                var windDirection = new WindDirection(hourly_units.wind_direction_10m, hourly.wind_direction_10m[i]);
                var wind = new WindData(windSpeed, windDirection);
                var humidity = new RelativeHumidityData(hourly_units.humidity_10m, hourly.relative_humidity_2m[i]);
                result.Add(new BaseWeatherData(curTime, temperature, wind, humidity));
            }
 
            return result;
        }
    }
}
