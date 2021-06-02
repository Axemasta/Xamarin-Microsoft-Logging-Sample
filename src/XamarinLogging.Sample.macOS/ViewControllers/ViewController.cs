using System;

using AppKit;
using Foundation;
using Microsoft.Extensions.Logging;

namespace XamarinLogging.Sample.macOS
{
    public partial class ViewController : NSViewController
    {
        private readonly ILogger _logger;

        public ViewController(IntPtr handle) : base(handle)
        {
            _logger = Logger.Instance;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            _logger.LogInformation("ViewDidLoad");
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
