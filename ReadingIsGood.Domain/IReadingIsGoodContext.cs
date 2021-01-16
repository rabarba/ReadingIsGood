using MongoDB.Driver;
using ReadingIsGood.Domain.Documents;

namespace ReadingIsGood.Domain
{
    public interface IReadingIsGoodContext
    {
        IMongoClient MongoClient { get; }
        IMongoCollection<Customer> Customers { get; }
        IMongoCollection<Product> Products { get; }
        IMongoCollection<CustomerOrder> CustomerOrders { get; }
        IMongoCollection<EventLog> EventLogs { get; }
    }
}
