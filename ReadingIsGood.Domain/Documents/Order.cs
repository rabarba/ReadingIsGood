using System;

namespace ReadingIsGood.Domain.Documents
{
    public class Order : Document
    {
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public short Quantity { get; set; }
        public DateTime OrderedAt { get; set; }
    }
}
