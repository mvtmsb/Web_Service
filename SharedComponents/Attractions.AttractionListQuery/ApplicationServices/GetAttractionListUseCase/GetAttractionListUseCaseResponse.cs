using Attractions.DomainObjects;
using Attractions.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attractions.ApplicationServices.GetAttractionListUseCase
{
    public class GetAttractionListUseCaseResponse : UseCaseResponse
    {
        public IEnumerable<Attraction> Attractions { get; }

        public GetAttractionListUseCaseResponse(IEnumerable<Attraction> attractions) => Attractions = attractions;
    }
}
