
namespace ConsoleInteractionWithApi.IOModule.Command
{
    internal class ProxyCommand: ConsoleCommand
    {
        public string Message { get; private set; }
        public ProxyCommand(string message) 
        {
            Message = message;
        }
    }
}
