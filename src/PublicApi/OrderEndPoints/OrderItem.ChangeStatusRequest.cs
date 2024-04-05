namespace Microsoft.eShopWeb.PublicApi.OrderEndPoints;

public class ChangeStatusRequest : BaseRequest
{
    public int OrderId { get; init; }

    public ChangeStatusRequest(int orderId)
    {
        OrderId = orderId;
    }
    
}
