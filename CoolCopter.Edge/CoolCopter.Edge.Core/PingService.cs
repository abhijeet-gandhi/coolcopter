using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using Grpc.Net.Client;
using static CoolCopter.Edge.Core.Edge;
using System.Net.Sockets;

namespace CoolCopter.Edge.Core
{
    public interface IPingService
    {
        bool Ping(Guid id, Guid key);
    }

    public class PingService : IPingService
    {
        private readonly EdgeClient Client;
        public PingService(GrpcChannel channel)
        {
            Client = new EdgeClient(channel);
        }

        public bool Ping(Guid id, Guid key)
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            var ip = Dns.GetHostEntry(hostName).AddressList
                .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
            var request = new PingRequest
            {
                Id = id.ToString().ToLower(),
                Key = key.ToString().ToLower(),
                Timestamp = DateTime.UtcNow.ToLongTimeString(),
                PrivateIP = ip.ToString()
            };

            var response = Client.Ping(request);
            Console.WriteLine($"Received response {request.PrivateIP}");
            return true;
        }
    }
}
