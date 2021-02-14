using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using HttpCore.Web;

namespace HttpCore.Http
{
    public class Response
    {
        private readonly Dictionary<int, string> responseCodes = new Dictionary<int, string>()
        {
            { 200, "OK" }, { 301, "Permanently Moved" }, { 302, "Temporarily Moved" }, { 403, "Forbidden" }, { 404, "Not Found" }, { 418, "I'm a Teapot" },
            { 500, "Internal Server Error" }, { 502, "Bad Gateway" }, { 503, "Service Temporarily Unavailable" }
        };

        public int Status { get; set; }

        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }

        public static Response FromFilePath(string filePath)
        {
            int status = 200;

            if (!File.Exists(filePath))
                status = 404;

            string filePathExtension = Path.GetExtension(filePath);
            return new Response(status, Mime.GetFromExtension(filePathExtension));
        }

        private Response()
        {
            Headers = new Dictionary<string, string>();
        }

        public Response(int status, string contentType = null)
            : this()
        {
            Status = status;

            if (contentType != null)
                Headers.Add("Content-Type", contentType);
        }

        public void Send(NetworkStream stream)
        {
            stream.Write(Encoding.ASCII.GetBytes(ToString()));
            stream.Flush();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"HTTP/1.1 {Status.ToString()} {responseCodes[Status] ?? string.Empty}\r\n");

            foreach (KeyValuePair<string, string> header in Headers)
                builder.Append($"{header.Key}: {header.Value}\r\n");

            builder.Append("\r\n");
            builder.Append(Body);

            return builder.ToString();
        }
    }
}
