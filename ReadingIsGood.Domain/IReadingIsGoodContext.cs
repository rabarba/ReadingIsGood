using MongoDB.Driver;
using ReadingIsGood.Domain.Documents;

namespace ReadingIsGood.Domain
{
    public interface IReadingIsGoodContext
    {
        IMongoCollection<Customer> Customers { get; }
        IMongoCollection<Product> Products { get; }
        IMongoCollection<Order> Orders { get; }
    }
}
