using System;
using System.IO;
using Foundation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using XamarinLogging.Sample.macOS.Helpers;

namespace XamarinLogging.Sample.macOS
{
    public class Logger : ILogger
    {
        public static Logger Instance { get; } = new Logger();

        private ILogger _logger;

        private string _bundleName;

        private Logger()
        {
            var bundleName = NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleName") as NSString;

            _bundleName = bundleName.ToString();

            TryInitializeLogger();
        }

        private void TryInitializeLogger()
        {
            try
            {
                InitializeLogger();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured initializing logger");
                Console.WriteLine(ex);
                System.Diagnostics.Debugger.Break();
            }
        }

        private void InitializeLogger()
        {
            IFileProvider provider = new ConfigFileProvider();

            /* ConfigurationBuilder will look for appsettings.json in the current executing location. 
             * This is within the macOS bundle and causes a FileNotFoundException to be thrown. I wasn't
             * able to get this working with the config file embedded as a resource, instead I use a custom
             * file provider to specify the location of the file (which should be in the same directory as
             * the actual mac .app
             */

            var configuration = new ConfigurationBuilder()
                    .AddJsonFile(provider, "appsettings.json", false, false)
                    .Build();

            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration.GetSection("Logging"));

                var baseDirectory = MacHelper.GetBundleLocationString();

                // the "standard" provider which logs all messages with severity warning or above to 'warn+err.log' (see appsettings.json for configuration settings)
                builder.AddFile(o => o.RootPath = baseDirectory);

                builder.AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddConsole();
            });

            ServiceProvider sp = services.BuildServiceProvider();

            _logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger(_bundleName);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _logger.BeginScope(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}
