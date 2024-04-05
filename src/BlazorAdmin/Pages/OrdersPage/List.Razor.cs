using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorShared.Interfaces;
using BlazorShared.Models;

namespace BlazorAdmin.Pages.OrdersPage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IOrderService OrderService { get; set; }
    
    private List<OrderList> orderList = new List<OrderList>();
    private List<OrderItem> orderItems = new();
    
    private Details DetailsComponent { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            orderList= await OrderService.List();

            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }
    
    private async Task ReloadOrderItems()
    {
        orderList = await OrderService.List();
        StateHasChanged();
    }
    private async void DetailsClick(int id,bool status)
    {
        await DetailsComponent.Open(id,status);
    }
    
    
}
