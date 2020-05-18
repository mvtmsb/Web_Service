using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Attractions.DomainObjects.Ports
{
    public interface IReadOnlyAttractionRepository
    {
        Task<Attraction> GetAttraction(long id);

        Task<IEnumerable<Attraction>> GetAllAttractions();

        Task<IEnumerable<Attraction>> QueryAttractions(ICriteria<Attraction> criteria);

    }

    public interface IAttractionRepository
    {
        Task AddAttraction(Attraction attraction);

        Task RemoveAttraction(Attraction attraction);

        Task UpdateAttraction(Attraction attraction);
    }
}
