using MongoDB.Driver;
using ReadingIsGood.Domain;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Settings;

namespace ReadingIsGood.Infrastructure
{
    /// <summary>
    /// MongoDb Context
    /// </summary>
    public class ReadingIsGoodContext : IReadingIsGoodContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public ReadingIsGoodContext(IMongoDbSettings mongoDbSettings)
        {
            _mongoDatabase = new MongoClient(mongoDbSettings.ConnectionString).GetDatabase(mongoDbSettings.DatabaseName);
        }

        public IMongoCollection<Product> Products => _mongoDatabase.GetCollection<Product>("Products");
        public IMongoCollection<Customer> Customers => _mongoDatabase.GetCollection<Customer>("Customers");
        public IMongoCollection<Order> Orders => _mongoDatabase.GetCollection<Order>("Orders");
        public IMongoCollection<EventLog> EventLogs => _mongoDatabase.GetCollection<EventLog>("EventLogs");
    }
}
