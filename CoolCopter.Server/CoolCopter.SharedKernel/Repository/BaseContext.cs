using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CoolCopter.SharedKernel.Repository
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseContext<TContext>
      : DbContext where TContext : DbContext
    {
        public BaseContext(DbContextOptions<TContext> options) : base(options)
        {
            //Database.SetInitializer<TContext>(null);
        }

        public BaseContext()
        {

        }
    }
}
