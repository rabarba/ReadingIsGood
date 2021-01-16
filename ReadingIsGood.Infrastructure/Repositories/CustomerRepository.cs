using MongoDB.Driver;
using ReadingIsGood.Domain;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Exceptions;
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
            // Duplicate Control
            // Email or Phone Numer is registered validation control throw exception 
            var registeredCustomer = await GetCustomerByEmailAndPhoneAsync(customer.Email, customer.Phone);
            if (registeredCustomer != null)
            {
                throw new ApiException("You have already account", System.Net.HttpStatusCode.BadRequest);
            }

            await _context.Customers.InsertOneAsync(customer);
            return customer.Id.ToString();
        }

        public async Task<Customer> GetCustomerAsync(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, id);
            var customer = await _context.Customers.Find(filter).FirstOrDefaultAsync();
            return customer;
        }

        private async Task<Customer> GetCustomerByEmailAndPhoneAsync(string email,string phone)
        {
            var filter  = Builders<Customer>.Filter.Eq(x => x.Email, email);
                filter |= Builders<Customer>.Filter.Eq(x => x.Phone, phone);

            var customer = await _context.Customers.Find(filter).FirstOrDefaultAsync();
            return customer;
        }
    }
}
