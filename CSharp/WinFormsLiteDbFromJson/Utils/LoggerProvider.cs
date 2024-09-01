
using Microsoft.Extensions.Logging;

namespace WinFormsLiteDbFromJson.Utils
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly StreamWriter _logWriter;

        public LoggerProvider(StreamWriter logWriter)
        {
            _logWriter = logWriter ?? throw new ArgumentNullException(nameof(logWriter));
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new LogWriter(_logWriter, categoryName);
        }

        public void Dispose()
        {
            _logWriter?.Dispose();
        }
    }
}
