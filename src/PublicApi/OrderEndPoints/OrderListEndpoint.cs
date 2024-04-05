using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlazorShared.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndPoints;

public class OrderListEndpoint: IEndpoint<IResult, IRepository<Order>>
{
    public IMapper _mapper { get; set; }
    
    public async Task<IResult> HandleAsync(IRepository<Order> orderListRepository)
    {
        var response = new ListOrderResponse();

        var orders = new AllOrdersWithItemsSpecification();

        var items = await orderListRepository.ListAsync(orders);
        
        response.OrderList = items.Select(x=>new OrderDto()
        {
            Id = x.Id,
            BuyerId = x.BuyerId,
            IsApproved = x.IsApproved,
            OrderDate = x.OrderDate,
            OrderItems = x.OrderItems.ToList(),
            Total = x.Total()
        }).ToList();
        
        return Results.Ok(response.OrderList);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/order-list",
                async (IRepository<Order> orderListRepository) =>
                {
                    return await HandleAsync(orderListRepository);
                })
            .Produces<ListOrderResponse>()
            .WithTags("OrderListEndpoints");
    }
}
