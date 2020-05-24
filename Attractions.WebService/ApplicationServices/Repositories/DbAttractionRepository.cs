using Attractions.ApplicationServices.Ports.Gateways.Database;
using Attractions.DomainObjects;
using Attractions.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Attractions.ApplicationServices.Repositories
{
    public class DbAttractionRepository : IReadOnlyAttractionRepository,
                                     IAttractionRepository
    {
        private readonly IAttractionDatabaseGateway _databaseGateway;

        public DbAttractionRepository(IAttractionDatabaseGateway databaseGateway)
            => _databaseGateway = databaseGateway;

        public async Task<Attraction> GetAttraction(long id)
            => await _databaseGateway.GetAttraction(id);

        public async Task<IEnumerable<Attraction>> GetAllAttractions()
            => await _databaseGateway.GetAllAttractions();

        public async Task<IEnumerable<Attraction>> QueryAttractions(ICriteria<Attraction> criteria)
            => await _databaseGateway.QueryAttractions(criteria.Filter);

        public async Task AddAttraction(Attraction attraction)
            => await _databaseGateway.AddAttraction(attraction);

        public async Task RemoveAttraction(Attraction attraction)
            => await _databaseGateway.RemoveAttraction(attraction);

        public async Task UpdateAttraction(Attraction attraction)
            => await _databaseGateway.UpdateAttraction(attraction);
    }
}
