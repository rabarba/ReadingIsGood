using MongoDB.Driver;
using ReadingIsGood.Domain;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Settings;
using System;

namespace ReadingIsGood.Infrastructure
{
    /// <summary>
    /// MongoDb Context
    /// </summary>
    public class ReadingIsGoodContext : IReadingIsGoodContext
    {
        public IMongoClient MongoClient { get; }
        private readonly string _databaseName;

        public ReadingIsGoodContext(IMongoDbSettings mongoDbSettings)
        {
            MongoClient = new MongoClient(mongoDbSettings.ConnectionString);
            _databaseName = mongoDbSettings.DatabaseName;
        }

        public IMongoCollection<Product> Products => MongoClient.GetDatabase(_databaseName, new MongoDatabaseSettings
        {
            ReadConcern = ReadConcern.Majority,
            WriteConcern = new WriteConcern(WriteConcern.WMode.Majority, TimeSpan.FromMilliseconds(1000))
        }).GetCollection<Product>("Products");

        public IMongoCollection<Customer> Customers => MongoClient.GetDatabase(_databaseName).GetCollection<Customer>("Customers");
        public IMongoCollection<CustomerOrder> CustomerOrders => MongoClient.GetDatabase(_databaseName).GetCollection<CustomerOrder>("CustomerOrders");
        public IMongoCollection<EventLog> EventLogs => MongoClient.GetDatabase(_databaseName).GetCollection<EventLog>("EventLogs");
    }
}
