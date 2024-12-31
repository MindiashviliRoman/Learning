
using ConsoleInteractionWithApi.DataModule;

namespace ConsoleInteractionWithApi.ApiServices
{
    internal interface IMeteoProvider: IDisposable
    {
        Task<BaseWeatherData> GetCurrentWeatherAsync(float latitude, float longitude);
        Task<List<BaseWeatherData>> GetForecastWeatherAsync(float latitude, float longitude, string dateFrom, string dateTo);

    }
}
