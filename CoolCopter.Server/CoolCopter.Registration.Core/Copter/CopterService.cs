using CoolCopter.Registration.Core.Copter.CopterAggregate;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoolCopter.Registration.Core.Copter
{
    public interface ICopterService : IDisposable
    {
        Copters GetCopter(Guid key);
        bool LastSeen(Guid key, Guid id);
    }

    public class CopterService : ICopterService
    {
        public readonly ICopterRepository Repository;

        public CopterService(ICopterRepository repository)
        {
            Repository = repository;
        }

        public Copters GetCopter(Guid key)
        {
            var copter = Repository.AllWithNoTracking.FirstOrDefault(x => x.Key == key);
            return copter;
        }

        public bool LastSeen(Guid key, Guid id)
        {
            var copter = Repository.All.First(x => x.Key == key && x.Id == id);
            copter.LastConnectedTimestamp = DateTime.UtcNow;
            Repository.Update(copter);
            return true;
        }

        public void Dispose()
        {
            Repository?.Dispose();
        }
    }
}
