using ReadingIsGood.Domain;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Interfaces;
using ReadingIsGood.Domain.Settings;
using System.Threading.Tasks;

namespace ReadingIsGood.Infrastructure.Repositories
{
    public class EventLogRepository : IEventLogRepository
    {
        private readonly IReadingIsGoodContext _context;
        public EventLogRepository(IMongoDbSettings mongoDbSettings)
        {
            _context = new ReadingIsGoodContext(mongoDbSettings);
        }

        public async Task CreateEventLogAsync(EventLog eventLog)
        {
            await _context.EventLogs.InsertOneAsync(eventLog);
        }
    }
}
