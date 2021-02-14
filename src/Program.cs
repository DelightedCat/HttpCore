﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using HttpCore.Http;

namespace HttpCore
{
    public class Program
    {
        private static Logger _logger;

        private static Config _config;

        private static void Main()
        {
            _logger = new Logger();

            string configFilePath = Path.Combine(Environment.CurrentDirectory, "data", "config.json");
            _logger.Write($"Reading config from {configFilePath}...");

            if (File.Exists(configFilePath))
            {
                string text = File.ReadAllText(configFilePath);
                _config = JsonConvert.DeserializeObject<Config>(text);
            }

            else
            {
                _config = Config.Default;
                string text = JsonConvert.SerializeObject(_config);

                Directory.CreateDirectory(Path.GetDirectoryName(configFilePath));

                using (StreamWriter writer = File.CreateText(configFilePath))
                    writer.Write(text);

                _logger.Write($"Created new config file at {configFilePath}", ConsoleColor.Yellow);
            }

            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), _config.Port);
            listener.Start();

            _logger.Write($"Server is now listening on port {_config.Port.ToString()}!");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                _logger.Write($"Incoming connection from {client.Client.RemoteEndPoint}");

                if (!stream.CanRead || !stream.CanWrite)
                    return;

                Request request = Request.FromStream(stream);

                if (request == null)
                    continue;

                _logger.Write($"{request.Method} {request.Uri}");

                string requestFileName = request.Uri == "/" ? _config.Index : request.Uri.Substring(1).Replace('/', Path.DirectorySeparatorChar);
                string requestFilePath = Path.Combine(_config.DocumentRoot, requestFileName);

                Response response = Response.FromFilePath(requestFilePath);

                response.Headers.Add("Server", "DelightedCat/1.0.0");
                response.Headers.Add("Connection", "keep-alive");

                if (File.Exists(requestFilePath))
                    response.Body = File.ReadAllText(requestFilePath);

                response.Send(stream);
                client.Close();
            }
        }
    }
}
