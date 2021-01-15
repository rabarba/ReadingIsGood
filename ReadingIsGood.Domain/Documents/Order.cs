using System;
using System.Collections.Generic;

namespace ReadingIsGood.Domain.Documents
{
    public class Order : Document
    {
        public string CustomerId { get; set; }
        public List<string> ProductId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderedAt { get; set; }
    }
}
