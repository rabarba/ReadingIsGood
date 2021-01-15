using MediatR;

namespace ReadingIsGood.Domain.Events
{
    public class CustomerCreatedEvent : INotification
    {
        public string CustomerId { get; set; }
    }
}
