using Attractions.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Attractions.ApplicationServices.Ports.Gateways.Database;

namespace Attractions.InfrastructureServices.Gateways.Database
{
    public class AttractionEFSqliteGateway : IAttractionDatabaseGateway
    {
        private readonly AttractionContext _attractionContext;

        public AttractionEFSqliteGateway(AttractionContext AttractionContext)
            => _attractionContext = AttractionContext;

        public async Task<Attraction> GetAttraction(long id)
           => await _attractionContext.Attractions.Where(at => at.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Attraction>> GetAllAttractions()
            => await _attractionContext.Attractions.ToListAsync();
          
        public async Task<IEnumerable<Attraction>> QueryAttractions(Expression<Func<Attraction, bool>> filter)
            => await _attractionContext.Attractions.Where(filter).ToListAsync();

        public async Task AddAttraction(Attraction attractionpoint)
        {
            _attractionContext.Attractions.Add(attractionpoint);
            await _attractionContext.SaveChangesAsync();
        }

        public async Task UpdateAttraction(Attraction attractionpoint)
        {
            _attractionContext.Entry(attractionpoint).State = EntityState.Modified;
            await _attractionContext.SaveChangesAsync();
        }

        public async Task RemoveAttraction(Attraction attractionpoint)
        {
            _attractionContext.Attractions.Remove(attractionpoint);
            await _attractionContext.SaveChangesAsync();
        }

    }
}
