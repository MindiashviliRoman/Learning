
namespace ConsoleInteractionWithApi.IOModule.Command
{
    internal class GetWeatherWithIntervalCommand: GetWeatherCommand
    {
        public DateTime DateFrom { get; private set; }
        public DateTime DateTo { get; private set; }

        public GetWeatherWithIntervalCommand(string command, float latitude, float longitude, DateTime dateFrom, DateTime dateTo)
            : base(command, latitude, longitude)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}
