using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarWars.Core.Models;

namespace StarWars.Data.EntityFramework
{
    public sealed class StarWarsContext : DbContext
    {
        public readonly ILogger Logger;

        public StarWarsContext() { }

        public StarWarsContext(DbContextOptions options, ILogger<StarWarsContext> logger) : base(options)
        {
            Logger = logger;
            Database.EnsureCreated();
        }

        public DbSet<Droid> Droids { get; set; }
    }
}
