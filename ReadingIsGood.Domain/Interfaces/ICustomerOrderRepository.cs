using ReadingIsGood.Domain.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Domain.Interfaces
{
    public interface ICustomerOrderRepository
    {
        Task<List<CustomerOrder>> GetCustomerOrdersAsync(string customerId);
        Task<CustomerOrder> GetCustomerOrderDetailAsync(string id);
        Task<CustomerOrder> CreateCustomerOrderAsync(CustomerOrder customerOrder);
    }
}
