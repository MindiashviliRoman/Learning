
using ConsoleInteractionWithApi.IOModule.Command;

namespace ConsoleInteractionWithApi.IOModule
{
    internal class ConsoleInputProvider : IInputProvider
    {
        #region IInputProvider
        public ConsoleCommand GetNextCommand()
        {
            var curLine = Console.ReadLine().Trim();
            try
            {
                var command = ConsoleCommand.ParseConsoleCommand(curLine);
                return command;
            }
            catch (Exception e)
            {
                return new ProxyCommand(e.Message);
            }
        }

        public bool IsQuiteCommand(ConsoleCommand command)
        {
            return command is QuiteCommand;
        }
        #endregion 
    }
}