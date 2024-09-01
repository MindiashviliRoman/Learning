
using Microsoft.Extensions.Logging;

namespace WinFormsLiteDbFromJson.Utils
{
    internal class LogWriter : ILogger
    {
        private StreamWriter _streamWriter;
        private string _categoryName;
        public LogWriter(StreamWriter streamWriter, string categoryName)
        {
            _streamWriter = streamWriter;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // Ensure that only information level and higher logs are recorded
            return logLevel >= LogLevel.Information;
        }

        public void Log<TState>(LogLevel logLevel, 
            EventId eventId, 
            TState state, 
            Exception? exception, 
            Func<TState, Exception?, string> formatter)
        {
            if(!IsEnabled(logLevel))
                return;

            if (formatter == null)
                return;

            var message = formatter(state, exception);

            _streamWriter.WriteLine($"[{logLevel}] [{_categoryName}] {message}");
            _streamWriter.Flush();
        }
    }
}
