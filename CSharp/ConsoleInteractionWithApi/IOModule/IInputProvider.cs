
using ConsoleInteractionWithApi.IOModule.Command;

namespace ConsoleInteractionWithApi.IOModule
{
    internal interface IInputProvider
    {
        bool IsQuiteCommand(ConsoleCommand command);
        ConsoleCommand GetNextCommand();
    }
}
