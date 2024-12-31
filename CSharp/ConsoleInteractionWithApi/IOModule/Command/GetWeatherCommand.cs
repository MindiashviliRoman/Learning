
namespace ConsoleInteractionWithApi.IOModule.Command
{
    internal class GetWeatherCommand: ConsoleCommand
    {
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }

        public GetWeatherCommand(string command, float latitude, float longitude) 
        {
            Command = command;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
