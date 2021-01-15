using ReadingIsGood.Domain.Documents;
using System.Threading.Tasks;

namespace ReadingIsGood.Domain.Interfaces
{
    public interface IEventLogRepository
    {
        /// <summary>
        /// Create a event log
        /// </summary>
        /// <param name="eventLog"></param>
        /// <returns></returns>
        Task CreateEventLogAsync(EventLog eventLog);
    }
}
