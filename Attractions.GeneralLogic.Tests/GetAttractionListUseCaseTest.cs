using Attractions.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Attractions.ApplicationServices.GetAttractionListUseCase;
using System.Linq.Expressions;
using Attractions.ApplicationServices.Ports;
using Attractions.DomainObjects.Ports;
using Attractions.ApplicationServices.Repositories;

namespace Attractions.WebService.Core.Tests
{
    public class GetAttractionListUseCaseTest
    {
        private InMemoryAttractionRepository CreateAttractiontRepository()
        {
            var repo = new InMemoryAttractionRepository(new List<Attraction> {
                new Attraction { Id = 1, NameObject="DiscO",Location= "Парк Культуры и отдыха «Сокольники»", Admission="срок действия истек, эксплуатация аттракциона не допускается", PeriodA="04.2019-12.2019"},
                new Attraction { Id = 2,  NameObject="Деревенский поезд",Location= "Парк Культуры и отдыха «Измайловский»", Admission="срок действия истек, эксплуатация аттракциона не допускается",  PeriodA="04.2019-12.2019"},
            });
            return repo;
        }
        [Fact]
        public void TestGetAllAttractions()
        {
            var useCase = new GetAttractionListUseCase(CreateAttractiontRepository());
            var outputPort = new OutputPort();
                        
            Assert.True(useCase.Handle(GetAttractionListUseCaseRequest.CreateAllAttractionsRequest(), outputPort).Result);
            Assert.Equal<int>(2, outputPort.Attractions.Count());
            Assert.Equal(new long[] { 1, 2}, outputPort.Attractions.Select(at => at.Id));
        }

        [Fact]
        public void TestGetAllAttractionsFromEmptyRepository()
        {
            var useCase = new GetAttractionListUseCase(new InMemoryAttractionRepository());
            var outputPort = new OutputPort();

            Assert.True(useCase.Handle(GetAttractionListUseCaseRequest.CreateAllAttractionsRequest(), outputPort).Result);
            Assert.Empty(outputPort.Attractions);
        }

        [Fact]
        public void TestGetAttraction()
        {
            var useCase = new GetAttractionListUseCase(CreateAttractiontRepository());
            var outputPort = new OutputPort();

            Assert.True(useCase.Handle(GetAttractionListUseCaseRequest.CreateAttractionRequest(2), outputPort).Result);
            Assert.Single(outputPort.Attractions, at => 2 == at.Id);
        }

        [Fact]
        public void TestTryGetNotExistingAttraction()
        {
            var useCase = new GetAttractionListUseCase(CreateAttractiontRepository());
            var outputPort = new OutputPort();

            Assert.True(useCase.Handle(GetAttractionListUseCaseRequest.CreateAttractionRequest(999), outputPort).Result);
            Assert.Empty(outputPort.Attractions);
        }
      
    }

    class OutputPort : IOutputPort<GetAttractionListUseCaseResponse>
    {
        public IEnumerable<Attraction> Attractions { get; private set; }

        public void Handle(GetAttractionListUseCaseResponse response)
        {
            Attractions = response.Attractions;
        }
    }
}
