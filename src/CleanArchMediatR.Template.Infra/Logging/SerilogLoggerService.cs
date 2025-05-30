using CleanArchMediatR.Template.Domain.Interfaces.Services;
using Serilog;

namespace CleanArchMediatR.Template.Infra.Logging
{
    public class SerilogLoggerService: ILoggerService
    {
        private readonly ILogger _logger;

        public SerilogLoggerService()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", 
                    rollingInterval: RollingInterval.Day, 
                    retainedFileCountLimit: 7,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"
                    )
                .CreateLogger();
        }

        public void LogInfo(string message) => _logger.Information(message);
        public void LogWarning(string message) => _logger.Warning(message);
        public void LogError(string message, Exception? ex = null) => _logger.Error(ex, message);
        public void LogDebug(string message) => _logger.Debug(message);
    }
}
