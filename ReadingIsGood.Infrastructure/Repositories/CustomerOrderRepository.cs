using MongoDB.Driver;
using ReadingIsGood.Domain;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Domain.Exceptions;
using ReadingIsGood.Domain.Interfaces;
using ReadingIsGood.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Infrastructure.Repositories
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly IReadingIsGoodContext _context;
        public CustomerOrderRepository(IMongoDbSettings mongoDbSettings)
        {
            _context = new ReadingIsGoodContext(mongoDbSettings);
        }
        public async Task<string> CreateCustomerOrderAsync(CustomerOrder customerOrder)
        {
            using var session = await _context.MongoClient.StartSessionAsync(new ClientSessionOptions { CausalConsistency = true });

            var productFilter = Builders<Product>.Filter.In(x => x.Id, customerOrder.Products.Select(x => x.Id));
            var productsInStock = await _context.Products.Find(productFilter).ToListAsync();

            if (!CheckProductStock(customerOrder.Products, productsInStock))
            {
                throw new ApiException("The product not found our stock sorry :(", System.Net.HttpStatusCode.BadRequest);
            }

            // Update Product Quantity in stock
            foreach (var productInStock in productsInStock)
            {
                UpdateDefinition<Product> updateDefinition = Builders<Product>.Update.Set(x => x.Quantity, productInStock.Quantity);
                _context.Products.UpdateOne(x => x.Id == productInStock.Id, updateDefinition);
            }

            await _context.CustomerOrders.InsertOneAsync(customerOrder);
            return customerOrder.Id;
        }
        private bool CheckProductStock(List<Product> orderedProducts, List<Product> productsInStock)
        {
            var orderedProductsDictionary = new Dictionary<string, short>();
            foreach (var orderedProduct in orderedProducts)
            {
                orderedProductsDictionary.Add(orderedProduct.Id, orderedProduct.Quantity);
            }

            foreach (var productInStock in productsInStock)
            {
                var orderedStockQuantity = orderedProductsDictionary[productInStock.Id];
                if (orderedStockQuantity > productInStock.Quantity) return false;

                productInStock.Quantity = Convert.ToInt16(productInStock.Quantity - orderedStockQuantity);
            }

            return true;
        }
    }
}
