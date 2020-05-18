using System.Threading.Tasks;
using System.Collections.Generic;
using Attractions.DomainObjects;
using Attractions.DomainObjects.Ports;
using Attractions.ApplicationServices.Ports;

namespace Attractions.ApplicationServices.GetAttractionListUseCase
{
    public class GetAttractionListUseCase : IGetAttractionListUseCase
    {
        private readonly IReadOnlyAttractionRepository _readOnlyAttractionRepository;

        public GetAttractionListUseCase(IReadOnlyAttractionRepository readOnlyAttractionRepository) 
            => _readOnlyAttractionRepository = readOnlyAttractionRepository;

        public async Task<bool> Handle(GetAttractionListUseCaseRequest request, IOutputPort<GetAttractionListUseCaseResponse> outputPort)
        {
            IEnumerable<Attraction> attractions = null;
            if (request.AttractionId != null)
            {
                var attraction = await _readOnlyAttractionRepository.GetAttraction(request.AttractionId.Value);
                attractions = (attraction != null) ? new List<Attraction>() { attraction } : new List<Attraction>();
                
            }
            else if (request.Location != null)
            {
                attractions = await _readOnlyAttractionRepository.QueryAttractions(new LocationCriteria(request.Location));
            }
            else
            {
                attractions = await _readOnlyAttractionRepository.GetAllAttractions();
            }
            outputPort.Handle(new GetAttractionListUseCaseResponse(attractions));
            return true;
        }
    }
}
