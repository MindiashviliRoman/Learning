
namespace ConsoleInteractionWithApi.IOModule.Command
{
    internal class SaveCommand: ConsoleCommand
    {
        public string FilePath { get; private set; }
        public SaveCommand(string command, string filePath)
        {
            Command = command;
            FilePath = filePath;
        }
    }
}
