
namespace ConsoleInteractionWithApi.IOModule.Command
{
    internal class ReadCommand:ConsoleCommand
    {
        public string FilePath { get; private set; }
        public int CountLines { get; private set; }
        public ReadCommand(string command, string filePath, int rowsCount)
        {
            Command = command;
            FilePath = filePath;
            CountLines = rowsCount;
        }
    }
}
