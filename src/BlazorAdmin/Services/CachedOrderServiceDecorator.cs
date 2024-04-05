using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace BlazorAdmin.Services;

public class CachedOrderServiceDecorator : IOrderService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly OrderListService _orderListService;
    private ILogger<CachedOrderServiceDecorator> _logger;

    public CachedOrderServiceDecorator(ILogger<CachedOrderServiceDecorator> logger, OrderListService orderListService, ILocalStorageService localStorageService)
    {
        _logger = logger;
        _orderListService = orderListService;
        _localStorageService = localStorageService;
    }

    public async Task<List<OrderList>> List()
    {
        //If you wont to cached order items open the comment line
        
        // string key = "orders";
        // var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<OrderList>>>(key);
        // if (cacheEntry != null)
        // {
        //     _logger.LogInformation("Loading items from local storage.");
        //     if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.UtcNow)
        //     {
        //         return cacheEntry.Value;
        //     }
        //     else
        //     {
        //         _logger.LogInformation($"Loading {key} from local storage.");
        //         await _localStorageService.RemoveItemAsync(key);
        //     }
        // }

        var items = await _orderListService.List();
        // var entry = new CacheEntry<List<OrderList>>(items);
        // await _localStorageService.SetItemAsync(key, entry);
        return items;
    }

    public async Task<List<OrderItem>> GetById(int id)
    {
        
        string key = "orderItems" + id;
        var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<OrderItem>>>(key);
        if (cacheEntry != null)
        {
            _logger.LogInformation("Loading items from local storage.");
            if (cacheEntry.DateCreated.AddMinutes(1) > DateTime.UtcNow)
            {
                return cacheEntry.Value;
            }
            else
            {
                _logger.LogInformation($"Loading {key} from local storage.");
                await _localStorageService.RemoveItemAsync(key);
            }
        }

        var items = await _orderListService.GetById(id);
        var entry = new CacheEntry<List<OrderItem>>(items);
        await _localStorageService.SetItemAsync(key, entry);
        return items;
    }

    public async Task<string> ChangeStatus(ChangeStatusOrderRequest req)
    {
        await _orderListService.ChangeStatus(req);
        return "Success";
    }
    
}
