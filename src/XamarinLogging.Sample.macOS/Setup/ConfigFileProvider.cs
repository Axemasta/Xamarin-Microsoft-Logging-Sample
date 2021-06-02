using System;
using System.IO;
using Foundation;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;
using XamarinLogging.Sample.macOS.Extensions;

namespace XamarinLogging.Sample.macOS
{
    public class ConfigFileProvider : IFileProvider
    {
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var nsSettingsUri = NSBundle.MainBundle.GetUrlForResource("appsettings", "json");

            var settingsUri = new Uri(nsSettingsUri.AbsoluteString, UriKind.Absolute);

            // Can we get a lmao
            //TODO: Make this code less hacky 😈
            var bundleLocation = settingsUri.GetParentUri().GetParentUri().GetParentUri().GetParentUri();

            var jsonPath = Path.Combine(bundleLocation.AbsolutePath, "appsettings.json");

            var info = new FileInfo(jsonPath);

            return new PhysicalFileInfo(info);
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }
    }
}
