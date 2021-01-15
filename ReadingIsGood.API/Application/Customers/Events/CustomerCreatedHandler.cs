using MediatR;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Events;
using ReadingIsGood.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Application.Customers.Events
{
    public class CustomerCreatedHandler : INotificationHandler<CustomerCreatedEvent>
    {
        private readonly IEventLogRepository _eventLogRepository;
        public CustomerCreatedHandler(IEventLogRepository eventLogRepository)
        {
            _eventLogRepository = eventLogRepository;
        }
        public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventLog = new EventLog
            {
                Message = $"{notification.CustomerId} customer was created."
            };

            await _eventLogRepository.CreateEventLogAsync(eventLog);
        }
    }
}
