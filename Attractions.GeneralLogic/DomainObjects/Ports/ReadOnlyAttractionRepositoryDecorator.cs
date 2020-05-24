using Attractions.DomainObjects;
using Attractions.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.DomainObjects.Repositories
{
    public abstract class ReadOnlyAttractionRepositoryDecorator : IReadOnlyAttractionRepository
    {
        private readonly IReadOnlyAttractionRepository _attractionRepository;

        public ReadOnlyAttractionRepositoryDecorator(IReadOnlyAttractionRepository attractionRepository)
        {
            _attractionRepository = attractionRepository;
        }

        public virtual async Task<IEnumerable<Attraction>> GetAllAttractions()
        {
            return await _attractionRepository?.GetAllAttractions();
        }

        public virtual async Task<Attraction> GetAttraction(long id)
        {
            return await _attractionRepository?.GetAttraction(id);
        }

        public virtual async Task<IEnumerable<Attraction>> QueryAttractions(ICriteria<Attraction> criteria)
        {
            return await _attractionRepository?.QueryAttractions(criteria);
        }
    }
}
