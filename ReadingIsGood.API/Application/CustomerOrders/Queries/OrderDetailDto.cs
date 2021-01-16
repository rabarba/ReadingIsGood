using System;
using System.Collections.Generic;

namespace ReadingIsGood.API.Application.CustomerOrders.Queries
{
    public class OrderDetailDto
    {
        public string CustomerOrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailProductDto> Products { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
    }

    public class OrderDetailProductDto
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public short Quantity { get; set; }
    }
}
