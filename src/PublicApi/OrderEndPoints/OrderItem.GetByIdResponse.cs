using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.PublicApi.OrderEndPoints;

public class GetByIdResponse : BaseResponse
{
    public GetByIdResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdResponse()
    {
    }

    public List<OrderItemDto> OrderItem { get; set; }
}
