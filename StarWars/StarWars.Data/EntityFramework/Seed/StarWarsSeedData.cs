using System.Linq;
using Microsoft.Extensions.Logging;
using StarWars.Core.Models;

namespace StarWars.Data.EntityFramework.Seed
{
    public static class StarWarsSeedData
    {
        public static void EnsureSeedData(this StarWarsContext db)
        {
            db.Logger.LogInformation("Seeding database");
            if (db.Droids.Any()) return;
            db.Logger.LogInformation("Seeding droids");
            var droid = new Droid
            {
                Name = "R2-D2"
            };
            db.Droids.Add(droid);
            db.SaveChanges();
        }
    }
}
