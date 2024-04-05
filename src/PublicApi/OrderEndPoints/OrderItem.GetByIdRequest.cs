namespace Microsoft.eShopWeb.PublicApi.OrderEndPoints;

public class GetByIdRequest : BaseRequest
{
    public int OrderId { get; init; }

    public GetByIdRequest(int orderId)
    {
        OrderId = orderId;
    }
}
