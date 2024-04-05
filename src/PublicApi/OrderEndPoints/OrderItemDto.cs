

using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.PublicApi.OrderEndPoints;

public class OrderItemDto
{
    public CatalogItemOrdered ItemOrdered { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get;  set; }
    public int OrderId { get; set; }
    public int Id { get; set; }
}
