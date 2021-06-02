using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AppKit;
using Foundation;
using Microsoft.Extensions.Logging;

namespace XamarinLogging.Sample.macOS
{
    public partial class ViewController : NSViewController
    {
        private List<LogLevel> _loggingLevels = new List<LogLevel>()
        {
            LogLevel.Trace,
            LogLevel.Debug,
            LogLevel.Information,
            LogLevel.Warning,
            LogLevel.Error,
            LogLevel.Critical
        };


        private readonly ILogger _logger;

        public ViewController(IntPtr handle) : base(handle)
        {
            _logger = Logger.Instance;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            logLevelSelector.RemoveAllItems();
            logLevelSelector.AddItems(_loggingLevels.Select(l => l.ToString()).ToArray());

            _logger.LogInformation("ViewDidLoad");
        }

        partial void logButtonPressed(NSButton sender)
        {
            var selectedLevel = logLevelSelector.SelectedItem;

            var logLevel = _loggingLevels?.FirstOrDefault(ll => ll.ToString() == selectedLevel.Title);

            if (logLevel is null)
            {
                Debug.WriteLine("No log level selected");
                return;
            }

            var message = messageTextField.StringValue;

            _logger.Log(logLevel.Value, message, null);

            messageTextField.StringValue = string.Empty;
        }

        public override void ViewDidAppear()
        {
            base.ViewDidAppear();

            this.View.Window.Title = "Logging Sample";
        }
    }
}
