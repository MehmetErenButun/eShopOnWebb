using System;
using System.Collections.Generic;

namespace BlazorShared.Models;

public class OrderList
{
    public DateTimeOffset OrderDate { get; set; }
    public string BuyerId { get; set; }
    public decimal Total { get; set; }
    public int  Id { get; set; }
    public bool IsApproved { get; set; }
    public  List<OrderItem> OrderItems{ get; set; }
    
}
