using System;
using System.Collections.Generic;
using System.Text;
using Attractions.ApplicationServices.Interfaces;

namespace Attractions.ApplicationServices.GetAttractionListUseCase
{
    public interface IGetAttractionListUseCase : IUseCase<GetAttractionListUseCaseRequest, GetAttractionListUseCaseResponse>
    {
    }
}
