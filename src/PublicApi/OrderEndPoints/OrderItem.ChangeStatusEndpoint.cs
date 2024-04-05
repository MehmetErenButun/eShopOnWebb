using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndPoints;

public class ChangeStatusEndpoint : IEndpoint<IResult, ChangeStatusRequest, IRepository<Order>>
{
   
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/change-status",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
                    (ChangeStatusRequest request, IRepository<Order> itemRepository) =>
                {
                    return await HandleAsync(request, itemRepository);
                })
            .Produces<ChangeStatusResponse>()
            .WithTags("OrderItemEndpoints");
    }

    public async Task<IResult> HandleAsync(ChangeStatusRequest request, IRepository<Order> itemRepository)
    {
        var response = new ChangeStatusResponse(request.CorrelationId());

        var existingItem = await itemRepository.GetByIdAsync(request.OrderId);
        if (existingItem == null)
        {
            return Results.NotFound();
        }

        existingItem.IsApproved = !existingItem.IsApproved;

        await itemRepository.UpdateAsync(existingItem);
        
        return Results.Ok(response.Status);
    }
}
