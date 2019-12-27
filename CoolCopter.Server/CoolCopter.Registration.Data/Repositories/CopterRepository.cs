using CoolCopter.Registration.Core.Copter;
using CoolCopter.Registration.Core.Copter.CopterAggregate;
using CoolCopter.SharedKernel.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoolCopter.Registration.Data.Repositories
{
    public class CopterRepository : EntityRepository<Copters>, ICopterRepository
    {
        public CopterRepository(RegistrationContext context)
            : base(context)
        {

        }
    }
}
