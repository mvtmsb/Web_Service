using Attractions.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.ApplicationServices.Ports.Gateways.Database
{
    public interface IAttractionDatabaseGateway
    {
        Task AddAttraction(Attraction attraction);

        Task RemoveAttraction(Attraction attraction);

        Task UpdateAttraction(Attraction attraction);

        Task<Attraction> GetAttraction(long id);

        Task<IEnumerable<Attraction>> GetAllAttractions();

        Task<IEnumerable<Attraction>> QueryAttractions(Expression<Func<Attraction, bool>> filter);

    }
}
