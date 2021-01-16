using System;
using System.Collections.Generic;

namespace ReadingIsGood.Domain.Documents
{
    public class CustomerOrder : Document
    {
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderedAt { get; set; }
    }
}
