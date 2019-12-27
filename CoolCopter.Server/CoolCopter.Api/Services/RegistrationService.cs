using CoolCopter.Registration.Core.Copter;
using Microsoft.Extensions.DependencyInjection;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolCopter.Api.Services
{
    public class RegistrationService : Registration.RegistrationBase
    {
        private readonly ILogger<RegistrationService> Logger;
        private readonly IServiceProvider ServiceProvider;
        public RegistrationService(ILogger<RegistrationService> logger,
            IServiceProvider provider)
        {
            Logger = logger;
            ServiceProvider = provider;
        }

        public override async Task<RegistrationResponse> Register(RegistrationRequest request, ServerCallContext context)
        {
            try
            {
                var response = new RegistrationResponse();
                response.Name = "NotFound/Exception";
                using var coperService = ServiceProvider.GetService<ICopterService>();
                var copter = coperService.GetCopter(Guid.Parse(request.Key));
                if (copter == null)
                    return response;
                response.Key = copter.Key.ToString().ToLower();
                response.Id = copter.Id.ToString().ToLower();
                response.Name = copter.Name;
                response.Internval = copter.Interval;
                return response;
            }
            catch (Exception exception)
            {
                var exc = exception;
                Logger.LogError($"Failed - {exception.Message}", exception);
            }
            return null;
        }
    }
}
