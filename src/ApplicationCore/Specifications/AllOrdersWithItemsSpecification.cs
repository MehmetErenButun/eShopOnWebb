﻿using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class AllOrdersWithItemsSpecification : Specification<Order>
{
    public AllOrdersWithItemsSpecification()
    {
        Query
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.ItemOrdered);
    }
}
