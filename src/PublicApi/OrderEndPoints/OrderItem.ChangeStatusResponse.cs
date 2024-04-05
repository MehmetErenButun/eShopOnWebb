using System;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.PublicApi.OrderEndPoints;

public class ChangeStatusResponse : BaseResponse
{
    public ChangeStatusResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ChangeStatusResponse()
    {
    }

    public string Status { get; set; } = "Approved";
}
