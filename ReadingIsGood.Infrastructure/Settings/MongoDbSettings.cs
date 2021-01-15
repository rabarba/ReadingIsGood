using ReadingIsGood.Domain.Settings;

namespace ReadingIsGood.Infrastructure.Settings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
