using Attractions.DomainObjects;
using Attractions.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Attractions.ApplicationServices.GetAttractionListUseCase
{
    public class LocationCriteria : ICriteria<Attraction>
    {
        public string Location { get; }

        public LocationCriteria(string location)
            => Location = location;

        public Expression<Func<Attraction, bool>> Filter
            => (at => at.Location == Location);
    }
}
