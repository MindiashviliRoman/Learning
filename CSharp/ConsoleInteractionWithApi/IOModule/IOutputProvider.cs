
using ConsoleInteractionWithApi.DataModule;

namespace ConsoleInteractionWithApi.IOModule
{
    internal interface IOutputProvider
    {
        void OutputLine(string data);
        void OutputLine(string data, ConsoleColor color);
        void OutputData(BaseWeatherData currentWeather);
        void OutputData(BaseWeatherData currentWeather, ConsoleColor color);
        void OutputData(List<BaseWeatherData> forecastWeather);
        void OutputData(List<BaseWeatherData> forecastWeather, ConsoleColor color);
    }
}
