using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using MinimalApi.Endpoint;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.eShopWeb.PublicApi.OrderEndPoints;

public class OrderItemGetByIdEndpoint: IEndpoint<IResult, GetByIdRequest, IRepository<OrderItem>>
{
    public async  Task<IResult> HandleAsync(GetByIdRequest request, IRepository<OrderItem> itemRepository)
    {
        var response = new GetByIdResponse(request.CorrelationId());

        var item = await itemRepository.ListAsync();

        var orderItems = item.Where(x => x.OrderId.Equals(request.OrderId));

        response.OrderItem = orderItems.Select(x => new OrderItemDto()
        {
            Id = x.Id,
            OrderId = x.OrderId,
            ItemOrdered = x.ItemOrdered,
            UnitPrice = x.UnitPrice,
            Units = x.Units
        }).ToList();
        
        return Results.Ok(response.OrderItem);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/order-items/{orderId}",
                async (int orderId, IRepository<OrderItem> itemRepository) =>
                {
                    return await HandleAsync(new GetByIdRequest(orderId), itemRepository);
                })
            .Produces<GetByIdCatalogItemResponse>()
            .WithTags("OrderItemEndpoints");
    }
}
