using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;

public interface IOrderService
{
    Task<List<OrderList>> List();
    Task<List<OrderItem>> GetById(int id);
    
    Task<string> ChangeStatus(ChangeStatusOrderRequest req);
    
}
