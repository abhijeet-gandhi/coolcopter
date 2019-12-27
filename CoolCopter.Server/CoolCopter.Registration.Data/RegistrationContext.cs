using CoolCopter.Registration.Core.Copter.CopterAggregate;
using CoolCopter.Registration.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoolCopter.Registration.Data
{
    public class RegistrationContext : DbContext
    {
        public RegistrationContext()
        {
        }

        public RegistrationContext(DbContextOptions<RegistrationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Copters> Copters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO: Uncomment while firing migrations
            //if (!optionsBuilder.IsConfigured)
            //{
            //    //optionsBuilder.UseSqlServer("Server = tcp:KMMStest.database.windows.net,1433; Initial Catalog =KMMSTest; Persist Security Info = False; User ID = tadmin; Password = Windows@12345; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");                
            //    optionsBuilder.UseSqlServer("Server = tcp:abdevsqlserver.database.windows.net,1433; Initial Catalog = registrationdb; Persist Security Info = False; User ID = tadmin; Password = Windows@12345; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            //    //optionsBuilder.UseSqlServer("Server = PU7L5X7MVF2; Initial Catalog = " + dbName + "; Persist Security Info = False; User ID = ab; Password = Windows@12345; MultipleActiveResultSets = False; Connection Timeout = 30;");
            //    //optionsBuilder.UseSqlServer("Server = tcp:10.167.177.18\\MSSQLSERVER,1433; Initial Catalog = ABDevDb; User ID = sa; Password = service; Integrated Security = false; ");
            //    //optionsBuilder.UseSqlServer("Server=tcp:beta-kmms-sqlserver.database.windows.net,1433;Initial Catalog=emptyDb;Persist Security Info=False;User ID=d0866a42-33a9-4478-bb1f-76e6f5ae39c7;Password=00cec448-8a1d-4084-870a-d0eab00875f7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CoptersMapping());

        }
    }
}