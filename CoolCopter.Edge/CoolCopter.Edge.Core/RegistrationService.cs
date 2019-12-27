using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Text;
using static CoolCopter.Edge.Core.Registration;

namespace CoolCopter.Edge.Core
{
    public interface IRegistrationService
    {
        RegistrationResponse GetEdgeId(Guid key);
    }

    public class RegistrationService : IRegistrationService
    {
        private readonly RegistrationClient Client;
        public RegistrationService(GrpcChannel channel)
        {
            Client = new RegistrationClient(channel);
        }

        public RegistrationResponse GetEdgeId(Guid key)
        {
            var response = Client.Register(new RegistrationRequest
            {
                Key = key.ToString().ToLower()
            });
            return response;
        }
    }
}
