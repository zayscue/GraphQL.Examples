using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarWars.Core.Data;
using StarWars.Core.Models;

namespace StarWars.Data.EntityFramework.Repositories
{
    public class DroidRepository : IDroidRepository
    {
        private readonly StarWarsContext _db;
        private readonly ILogger _logger;

        public DroidRepository(StarWarsContext db, ILogger<DroidRepository> logger)
        {
            if(db == null) throw new ArgumentNullException(nameof(db));
            _db = db;
            if(logger == null) throw new ArgumentNullException(nameof(logger));
            _logger = logger;
        }

        public async Task<Droid> Get(int id)
        {
            _logger.LogInformation("Get droid with id = {id}", id);
            return await _db.Droids.FirstOrDefaultAsync(droid => droid.Id == id);
        }
    }
}
