using CoolCopter.Registration.Core.Copter.CopterAggregate;
using CoolCopter.SharedKernel.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoolCopter.Registration.Core.Copter
{
    public interface ICopterRepository : IEntityRepository<Copters>
    {
    }
}
