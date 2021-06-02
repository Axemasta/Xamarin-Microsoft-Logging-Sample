using System;
using System.IO;
using XamarinLogging.Sample.macOS.Extensions;

namespace XamarinLogging.Sample.macOS.Helpers
{
    public class MacHelper
    {
        /// <summary>
        /// Gets The Executing Location Of The App Bundle
        /// .NET seems to return the location of the .dll
        /// which is nested inside the bundle, this will return
        /// the filesystem path for the .app bundle
        /// </summary>
        /// <returns></returns>
        public static Uri GetBundleLocation()
        {
            var baseDirectory = Directory.GetCurrentDirectory();

            var directoryUri = new Uri(baseDirectory);

            var bundleLocation = directoryUri.GetParentUri().GetParentUri().GetParentUri();

            return bundleLocation;
        }

        public static string GetBundleLocationString()
        {
            var bundleLocation = GetBundleLocation();

            return bundleLocation.AbsolutePath;
        }
    }
}
