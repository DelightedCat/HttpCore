using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace WebServer.Http
{
    public class Request
    {
        public bool IsValid { get; set; }

        public string Method { get; set; }
        public string Uri { get; set; }

        //[SkipLocalsInit] //Upgrade to .NET 5
        public static Request FromStream(NetworkStream stream)
        {
            //byte[] buffer = new byte[1024];

            Span<byte> buffer = stackalloc byte[1024];
            
            var builder = new StringBuilder();

            do
            {
                var length = stream.Read(buffer);
                
                builder.Append(Encoding.ASCII.GetString(buffer));
            }
            while (stream.DataAvailable);

            return Parse(builder.ToString());
        }

        private static Request Parse(string input)
        {
            var lines = input.Split("\r\n");
            
            var status = lines[0].Split(' ');

            return status.Length < 2 ? null : new Request(status[0], status[1]);
        }

        private Request(string method, string uri)
        {
            Method = method;
            
            Uri = uri;
        }
    }
}
