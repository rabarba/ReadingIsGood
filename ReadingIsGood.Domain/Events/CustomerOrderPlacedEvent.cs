using MediatR;
using ReadingIsGood.Domain.Documents;
using System.Collections.Generic;

namespace ReadingIsGood.Domain.Events
{
    public class CustomerOrderPlacedEvent : INotification
    {
        public string CustomerOrderId { get; set; }
        public List<OrderProduct> OrderedProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
