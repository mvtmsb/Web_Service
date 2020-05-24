using Attractions.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attractions.ApplicationServices.GetAttractionListUseCase
{
    public class GetAttractionListUseCaseRequest : IUseCaseRequest<GetAttractionListUseCaseResponse>
    {
        public string Location { get; private set; }
        public long? AttractionId { get; private set; }

        private GetAttractionListUseCaseRequest()
        { }

        public static GetAttractionListUseCaseRequest CreateAllAttractionsRequest()
        {
            return new GetAttractionListUseCaseRequest();
        }

        public static GetAttractionListUseCaseRequest CreateAttractionRequest(long attractionId)
        {
            return new GetAttractionListUseCaseRequest() { AttractionId = attractionId };
        }
        public static GetAttractionListUseCaseRequest CreateLocationAttractionsRequest(string location)
        {
            return new GetAttractionListUseCaseRequest() { Location = location };
        }
    }
}
