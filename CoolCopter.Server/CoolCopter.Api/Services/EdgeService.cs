using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using CoolCopter.Registration.Core.Copter;

namespace CoolCopter.Api
{
    public class EdgeService : Edge.EdgeBase
    {
        private readonly ILogger<EdgeService> _logger;
        private readonly IServiceProvider ServiceProvider;
        public EdgeService(ILogger<EdgeService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            ServiceProvider = serviceProvider;
        }

        public override async Task<PingResponse> Ping(PingRequest request, ServerCallContext context)
        {
            var response = new PingResponse();
            Console.WriteLine($"Ping recieved - {DateTime.Parse(request.Timestamp).ToLocalTime()}");
            using var service = ServiceProvider.GetService<ICopterService>();
            var result = service.LastSeen(Guid.Parse(request.Key), Guid.Parse(request.Id));
            response.Id = request.Id;
            return response;
        }
    }
}
