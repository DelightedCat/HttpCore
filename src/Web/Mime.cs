using System;
using System.Collections.Generic;
using System.Text;

namespace HttpCore.Web
{
    public static class Mime
    {
        private static readonly Dictionary<string, string> types = new Dictionary<string, string>()
        {
            { ".html", "text/html" }, { ".htm", "text/html" }, { ".css", "text/css" }
        };

        public static string GetFromExtension(string extension)
        {
            return !types.ContainsKey(extension) ? null : types[extension];
        }

        public static void RegisterType(string extension, string type)
        {
            types[extension] = type;
        }
    }
}
