using Attractions.DomainObjects;
using Attractions.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.ApplicationServices.Repositories
{
    public class InMemoryAttractionRepository : IReadOnlyAttractionRepository,
                                           IAttractionRepository
    {
        private readonly List<Attraction> _attractions = new List<Attraction>();

        public InMemoryAttractionRepository(IEnumerable<Attraction> attractions = null)
        {
            if (attractions != null)
            {
                _attractions.AddRange(attractions);
            }
        }

        public Task AddAttraction(Attraction attraction)
        {
            _attractions.Add(attraction);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Attraction>> GetAllAttractions()
        {
            return Task.FromResult(_attractions.AsEnumerable());
        }

        public Task<Attraction> GetAttraction(long id)
        {
            return Task.FromResult(_attractions.Where(at => at.Id == id).FirstOrDefault());
        }

        public Task<IEnumerable<Attraction>> QueryAttractions(ICriteria<Attraction> criteria)
        {
            return Task.FromResult(_attractions.Where(criteria.Filter.Compile()).AsEnumerable());
        }

        public Task RemoveAttraction(Attraction attraction)
        {
            _attractions.Remove(attraction);
            return Task.CompletedTask;
        }

        public Task UpdateAttraction(Attraction attraction)
        {
            var foundAttraction = GetAttraction(attraction.Id).Result;
            if (foundAttraction == null)
            {
                AddAttraction(attraction);
            }
            else
            {
                if (foundAttraction != attraction)
                {
                    _attractions.Remove(foundAttraction);
                    _attractions.Add(attraction);
                }
            }
            return Task.CompletedTask;
        }
    }
}
