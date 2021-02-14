using System;
using System.Net.Sockets;
using System.Text;

namespace HttpCore.Http
{
    public class Request
    {
        public bool IsValid { get; set; }

        public string Method { get; set; }
        public string Uri { get; set; }

        public static Request FromStream(NetworkStream stream)
        {
            Span<byte> buffer = stackalloc byte[1024];
            StringBuilder builder = new StringBuilder();

            do
            {
                int length = stream.Read(buffer);
                builder.Append(Encoding.ASCII.GetString(buffer));
            }
            while (stream.DataAvailable);

            return Parse(builder.ToString());
        }

        private static Request Parse(string input)
        {
            string[] lines = input.Split("\r\n");
            string[] status = lines[0].Split(' ');

            return status.Length < 2 ? null : new Request(status[0], status[1]);
        }

        private Request(string method, string uri)
        {
            Method = method;
            Uri = uri;
        }
    }
}
