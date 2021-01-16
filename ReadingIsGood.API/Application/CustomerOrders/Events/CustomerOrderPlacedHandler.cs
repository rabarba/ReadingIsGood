using MediatR;
using Newtonsoft.Json;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Events;
using ReadingIsGood.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Application.CustomerOrders.Events
{
    public class CustomerOrderPlacedHandler : INotificationHandler<CustomerOrderPlacedEvent>
    {
        private readonly IEventLogRepository _eventLogRepository;
        public CustomerOrderPlacedHandler(IEventLogRepository eventLogRepository)
        {
            _eventLogRepository = eventLogRepository;
        }
        public async Task Handle(CustomerOrderPlacedEvent notification, CancellationToken cancellationToken)
        {
            var eventLog = new EventLog
            {
                Message = $"{notification.CustomerOrderId} customer order was created.",
                Data = JsonConvert.SerializeObject(notification)
            };

            await _eventLogRepository.CreateEventLogAsync(eventLog);
        }
    }
}
