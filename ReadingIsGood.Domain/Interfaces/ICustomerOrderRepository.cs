using ReadingIsGood.Domain.Documents;
using System.Threading.Tasks;

namespace ReadingIsGood.Domain.Interfaces
{
    public interface ICustomerOrderRepository
    {
        Task<string> CreateCustomerOrderAsync(CustomerOrder customerOrder);
    }
}
