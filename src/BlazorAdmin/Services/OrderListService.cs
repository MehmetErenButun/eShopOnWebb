using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace BlazorAdmin.Services;

public class OrderListService : IOrderService
{
    private readonly HttpService _httpService;
    private readonly ILogger<OrderListService> _logger;

    public OrderListService(ILogger<OrderListService> logger, HttpService httpService)
    {
        _logger = logger;
        _httpService = httpService;
    }

    public async Task<List<OrderList>> List()
    {
        _logger.LogInformation("Fetching order items from API.");
        
        var itemListTask = await _httpService.HttpGet<List<OrderList>>($"order-list");
        var items = itemListTask;
   
        return items;
    }

    public async Task<List<OrderItem>> GetById(int id)
    {
        _logger.LogInformation("Fetching get by id item from API.");
        
        var itemListTask = await _httpService.HttpGet<List<OrderItem>>($"order-items/{id}");
        var items = itemListTask;
        foreach (var item in items)
        {
            _logger.LogInformation("bababasd. {Count}",item.UnitPrice);
        }
      
        return items;
    }

    public async Task<string> ChangeStatus(ChangeStatusOrderRequest req)
    {
        return (await _httpService.HttpPut<string>("change-status", req));
    }
}
