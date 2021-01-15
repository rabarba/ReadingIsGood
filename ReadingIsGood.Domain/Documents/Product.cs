namespace ReadingIsGood.Domain.Documents
{
    public class Product : Document
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short Quantity { get; set; }
    }
}
