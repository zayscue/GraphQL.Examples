using System.Threading.Tasks;
using StarWars.Core.Models;

namespace StarWars.Core.Data
{
    public interface IDroidRepository
    {
        Task<Droid> Get(int id);
    }
}
