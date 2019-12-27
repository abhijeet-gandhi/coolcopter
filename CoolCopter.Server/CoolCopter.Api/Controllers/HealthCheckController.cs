using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolCopter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> Logger;
        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            Logger = logger;
        }

        public bool Get()
        {
            Logger.LogDebug($"Invoked Get in {nameof(HealthCheckController)}");
            return true;
        }
    }
}
