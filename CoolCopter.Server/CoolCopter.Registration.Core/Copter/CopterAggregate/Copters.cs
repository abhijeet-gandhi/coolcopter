using CoolCopter.SharedKernel.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoolCopter.Registration.Core.Copter.CopterAggregate
{
    public class Copters : Entity<Guid>, IAggregateRoot
    {
        public Copters()
        {

        }

        public string Name { get; set; }
        public Guid Key { get; set; }
        public DateTime LastConnectedTimestamp { get; set; }
        public int Interval { get; set; }
    }
}
