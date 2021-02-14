using System.Collections.Generic;

namespace HttpCore
{
    public static class Context
    {
        public static readonly Dictionary<int, string> ResponseCodes;
        public static readonly Dictionary<string, string> MimeTypes;

        static Context()
        {
            ResponseCodes = new Dictionary<int, string>()
            {
                { 200, "OK" },
                { 301, "Permanently Moved" },
                { 302, "Temporarily Moved" },
                { 403, "Forbidden" },
                { 404, "Not Found" },
                { 418, "I'm a Teapot" },
                { 500, "Internal Server Error" },
                { 502, "Bad Gateway" },
                { 503, "Service Temporarily Unavailable" },
            };

            MimeTypes = new Dictionary<string, string>()
            {
                { ".html", "text/html" },
                { ".htm", "text/html" },
                { ".css", "text/css" },
            };
        }

        public static string GetResponseCode(int status, string fallback)
        {
            return ResponseCodes.ContainsKey(status) ? ResponseCodes[status] : fallback;
        }

        public static string GetMimeType(string extension, string fallback)
        {
            return MimeTypes.ContainsKey(extension) ? MimeTypes[extension] : fallback;
        }
    }
}
