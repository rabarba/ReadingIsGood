using System;
using System.Collections.Generic;

namespace ReadingIsGood.API.Application.CustomerOrders.Queries
{
    public class OrderDto
    {
        public List<OrderDtoItem> Orders { get; set; }
    }

    public class OrderDtoItem
    {
        public string Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
