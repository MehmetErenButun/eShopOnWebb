using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.PublicApi.OrderEndPoints;

public class OrderDto
{
    public DateTimeOffset OrderDate { get; set; }
    public string BuyerId { get; set; }
    public decimal Total { get; set; }
    public int Id { get; set; }
    public bool IsApproved { get; set; }
    public  List<OrderItem> OrderItems{ get; set; }
}
