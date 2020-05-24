using Attractions.ApplicationServices.Ports.Cache;
using Attractions.DomainObjects;
using Attractions.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Attractions.InfrastructureServices.Repositories
{
    public class NetworkAttractionRepository : NetworkRepositoryBase, IReadOnlyAttractionRepository
    {
        private readonly IDomainObjectsCache<Attraction> _attractionCache;

        public NetworkAttractionRepository(string host, ushort port, bool useTls, IDomainObjectsCache<Attraction> attractionCache)
            : base(host, port, useTls)
            => _attractionCache = attractionCache;

        public async Task<Attraction> GetAttraction(long id)
            => CacheAndReturn(await ExecuteHttpRequest<Attraction>($"attractions/{id}"));

        public async Task<IEnumerable<Attraction>> GetAllAttractions()
            => CacheAndReturn(await ExecuteHttpRequest<IEnumerable<Attraction>>($"attractions"), allObjects: true);

        public async Task<IEnumerable<Attraction>> QueryAttractions(ICriteria<Attraction> criteria)
            => CacheAndReturn(await ExecuteHttpRequest<IEnumerable<Attraction>>($"attractions"), allObjects: true)
               .Where(criteria.Filter.Compile());


        private IEnumerable<Attraction> CacheAndReturn(IEnumerable<Attraction> attractions, bool allObjects = false)
        {
            if (allObjects)
            {
                _attractionCache.ClearCache();
            }
            _attractionCache.UpdateObjects(attractions, DateTime.Now.AddDays(1), allObjects);
            return attractions;
        }

        private Attraction CacheAndReturn(Attraction attraction)
        {
            _attractionCache.UpdateObject(attraction, DateTime.Now.AddDays(1));
            return attraction;
        }
    }
}
