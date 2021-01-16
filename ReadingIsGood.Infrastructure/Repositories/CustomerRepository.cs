using MongoDB.Driver;
using ReadingIsGood.Domain;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Interfaces;
using ReadingIsGood.Domain.Settings;
using System.Threading.Tasks;

namespace ReadingIsGood.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IReadingIsGoodContext _context;
        public CustomerRepository(IMongoDbSettings mongoDbSettings)
        {
            _context = new ReadingIsGoodContext(mongoDbSettings);
        }

        public async Task<string> CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.InsertOneAsync(customer);
            return customer.Id.ToString();
        }

        public async Task<Customer> GetCustomerAsync(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, id);
            var customer = await _context.Customers.Find(filter).FirstOrDefaultAsync();
            return customer;
        }
    }
}
