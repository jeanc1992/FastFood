using FastFood.Infrastructure.Models.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.SystemConsole.Themes;
namespace FastFood.Infrastructure.Extensions
{
    public static class LoggingExtensions
    {
        public const string LoggingFormat = "{Timestamp:dd-MM-yyyy HH:mm:ss.fff} ({ThreadId}) [{Level}] {Message:lj}{NewLine}{Exception}";
        public static void SetupLogging(this List<FileToLog> logs, string logsPath = null, bool verbose = false)
        {
            logs = logs.GroupBy(l => l.AssemblyFullName).Select(g => g.First()).ToList();

            string basePath = logsPath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            if (string.IsNullOrWhiteSpace(basePath))
                throw new ArgumentNullException(nameof(logsPath), "You need to provide a base path for the logs");

            var loggingConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Debug(outputTemplate: LoggingFormat)
                .WriteTo.Console(outputTemplate: LoggingFormat, theme: AnsiConsoleTheme.Literate);

            if (verbose)
                loggingConfig.MinimumLevel.Verbose();

            var tenMb = 10000000;
            foreach (var kvp in logs)
            {
                string filename = $"{kvp.LogFileName}_.txt";
                if (kvp.Filtered)
                {
                    loggingConfig.WriteTo
                        .Logger(l => l.Filter.ByIncludingOnly(Matching.FromSource(kvp.AssemblyFullName))
                            .WriteTo.File(
                                Path.Combine(basePath, filename),
                                rollingInterval: RollingInterval.Day,
                                fileSizeLimitBytes: tenMb,
                                rollOnFileSizeLimit: true,
                                outputTemplate: LoggingFormat));
                }
                else
                {
                    loggingConfig.WriteTo
                        .Logger(l => l
                            .WriteTo.File(
                                Path.Combine(basePath, filename),
                                rollingInterval: RollingInterval.Day,
                                fileSizeLimitBytes: tenMb,
                                rollOnFileSizeLimit: true,
                                outputTemplate: LoggingFormat));
                }
            }
            Log.Logger = loggingConfig.CreateLogger();
        }
        public static IHostBuilder ConfigureAppLogging(this IHostBuilder builder)
           => builder.UseSerilog();
    }
}
