
using ConsoleInteractionWithApi.DataModule;

namespace ConsoleInteractionWithApi.IOModule
{
    internal class ConsoleOutputProvider : IOutputProvider
    {
        #region IOutputProvider
        public void OutputLine(string data)
        {
            Console.Write(data);
            Console.WriteLine();
        }
        public void OutputLine(string data, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(data);
            Console.ResetColor();
            Console.WriteLine();
        }

        public void OutputData(BaseWeatherData currentWeather)
        {
            Console.WriteLine(currentWeather);
        }
        public void OutputData(BaseWeatherData currentWeather, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(currentWeather);
            Console.ResetColor();
        }

        public void OutputData(List<BaseWeatherData> forecastWeather)
        {
            foreach(var weather in forecastWeather)
            { 
                Console.WriteLine(weather);
            }
        }

        public void OutputData(List<BaseWeatherData> forecastWeather, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (var weather in forecastWeather)
            {
                Console.WriteLine(weather);
            }
            Console.ResetColor();
        }

        #endregion
    }
}
