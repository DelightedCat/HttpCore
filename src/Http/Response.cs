using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace HttpCore.Http
{
    public class Response
    {
        public int Status { get; set; }

        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }

        public static Response FromFilePath(string filePath)
        {
            int status = 200;

            if (!File.Exists(filePath))
                status = 404;

            string filePathExtension = Path.GetExtension(filePath);
            return new Response(status, Context.GetMimeType(filePathExtension, "text/html")); // TODO: Update default content type here
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
            builder.Append($"HTTP/1.1 {Status.ToString()} {Context.GetResponseCode(Status, string.Empty)}\r\n");

            foreach (KeyValuePair<string, string> header in Headers)
                builder.Append($"{header.Key}: {header.Value}\r\n");

            builder.Append("\r\n");
            builder.Append(Body);

            return builder.ToString();
        }
    }
}
