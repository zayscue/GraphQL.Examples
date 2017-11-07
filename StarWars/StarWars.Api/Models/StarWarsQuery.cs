using System;
using GraphQL.Types;
using StarWars.Core.Data;

namespace StarWars.Api.Models
{
    public class StarWarsQuery : ObjectGraphType
    {
        private readonly IDroidRepository _droidRepository;

        public StarWarsQuery(IDroidRepository droidRepository)
        {
            if(droidRepository == null) throw new ArgumentNullException(nameof(droidRepository));
            _droidRepository = droidRepository;
            Field<DroidType>("hero", resolve: context => _droidRepository.Get(1));
        }
    }
}
