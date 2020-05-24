using Attractions.ApplicationServices.Ports.Cache;
using Attractions.DomainObjects;
using Attractions.DomainObjects.Ports;
using Attractions.DomainObjects.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.ApplicationServices.Repositories
{
    public class CachedReadOnlyAttractionRepository : ReadOnlyAttractionRepositoryDecorator
    {
        private readonly IDomainObjectsCache<Attraction> _attractionsCache;

        public CachedReadOnlyAttractionRepository(IReadOnlyAttractionRepository attractionRepository, 
                                             IDomainObjectsCache<Attraction> attractionsCache)
            : base(attractionRepository)
            => _attractionsCache = attractionsCache;

        public async override Task<Attraction> GetAttraction(long id)
            => _attractionsCache.GetObject(id) ?? await base.GetAttraction(id);

        public async override Task<IEnumerable<Attraction>> GetAllAttractions()
            => _attractionsCache.GetObjects() ?? await base.GetAllAttractions();

        public async override Task<IEnumerable<Attraction>> QueryAttractions(ICriteria<Attraction> criteria)
            => _attractionsCache.GetObjects()?.Where(criteria.Filter.Compile()) ?? await base.QueryAttractions(criteria);

    }
}
