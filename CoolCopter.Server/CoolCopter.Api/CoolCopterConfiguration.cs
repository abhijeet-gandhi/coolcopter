using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolCopter.Api
{
    public class CoolCopterConfiguration
    {
        public string RegistrationDbConnectionString { get; set; }
        public string SSLCertName { get; set; }
        public string SSLCertPassword { get; set; }
        public string Port { get; set; }
    }
}
