using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoolCopter.Edge.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoolCopter.Edge
{
    public class Worker : BackgroundService
    {
        private readonly IPingService PingService;
        private readonly IRegistrationService RegistrationService;
        private readonly EdgeConfig EdgeConfig;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger,
            EdgeConfig config,
            IPingService pingService,
            IRegistrationService registrationService)
        {
            _logger = logger;
            EdgeConfig = config;
            PingService = pingService;
            RegistrationService = registrationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var response = RegistrationService.GetEdgeId(Guid.Parse(EdgeConfig.Key));
            var id = Guid.Parse(response.Id);
            var key = Guid.Parse(response.Key);
            var interval = response.Internval < 1 ? 5 : response.Internval;
            while (!stoppingToken.IsCancellationRequested)
            {
                PingService.Ping(id, key);
                await Task.Delay(interval * 1000, stoppingToken);
            }
        }
    }
}
