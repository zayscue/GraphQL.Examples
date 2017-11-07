using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarWars.Core.Data;
using StarWars.Core.Models;

namespace StarWars.Data.InMemory
{
    public class DroidRepository : IDroidRepository
    {
        private readonly List<Droid> _droids = new List<Droid> {new Droid {Id = 1, Name = "R2-D2"}};

        public Task<Droid> Get(int id)
        {
            return Task.FromResult(_droids.FirstOrDefault(droid => droid.Id == id));
        }
    }
}
