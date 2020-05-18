using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Attractions.DomainObjects;
using Attractions.ApplicationServices.GetAttractionListUseCase;
using Attractions.InfrastructureServices.Presenters;

namespace Attractions.InfrastructureServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttractionsController : ControllerBase
    {
        private readonly ILogger<AttractionsController> _logger;
        private readonly IGetAttractionListUseCase _getAttractionListUseCase;

        public AttractionsController(ILogger<AttractionsController> logger,
                                IGetAttractionListUseCase getAttractionListUseCase)
        {
            _logger = logger;
            _getAttractionListUseCase = getAttractionListUseCase;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAttractions()
        {
            var presenter = new AttractionListPresenter();
            await _getAttractionListUseCase.Handle(GetAttractionListUseCaseRequest.CreateAllAttractionsRequest(), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("{attractionId}")]
        public async Task<ActionResult> GetAttraction(long attractionId)
        {
            var presenter = new AttractionListPresenter();
            await _getAttractionListUseCase.Handle(GetAttractionListUseCaseRequest.CreateAttractionRequest(attractionId), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("Locations/{location}")]
        public async Task<ActionResult> GetLocationFilter(string location)
        {
            var presenter = new AttractionListPresenter();
            await _getAttractionListUseCase.Handle(GetAttractionListUseCaseRequest.CreateLocationAttractionsRequest(location), presenter);
            return presenter.ContentResult;
        }
    }
}
