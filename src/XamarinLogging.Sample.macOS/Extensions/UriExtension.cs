using System;
using System.Linq;

namespace XamarinLogging.Sample.macOS.Extensions
{
    public static class UriExtension
    {
        public static string GetParentUriString(this Uri uri)
        {
            return uri.AbsoluteUri.Remove(uri.AbsoluteUri.Length - uri.Segments.Last().Length);
        }

        public static Uri GetParentUri(this Uri uri)
        {
            var path = uri.AbsoluteUri.Remove(uri.AbsoluteUri.Length - uri.Segments.Last().Length);

            return new Uri(path, UriKind.Absolute);
        }
    }
}
