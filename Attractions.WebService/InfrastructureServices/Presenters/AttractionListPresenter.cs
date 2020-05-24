using Attractions.ApplicationServices.GetAttractionListUseCase;
using System.Net;
using Newtonsoft.Json;
using Attractions.ApplicationServices.Ports;

namespace Attractions.InfrastructureServices.Presenters
{
    public class AttractionListPresenter : IOutputPort<GetAttractionListUseCaseResponse>
    {
        public JsonContentResult ContentResult { get; }

        public AttractionListPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GetAttractionListUseCaseResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            ContentResult.Content = response.Success ? JsonConvert.SerializeObject(response.Attractions) : JsonConvert.SerializeObject(response.Message);
        }
    }
}
