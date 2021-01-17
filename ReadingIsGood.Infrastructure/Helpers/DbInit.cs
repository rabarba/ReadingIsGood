using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using ReadingIsGood.Domain.Documents;
using ReadingIsGood.Infrastructure.Settings;
using System.Collections.Generic;

namespace ReadingIsGood.Infrastructure.Helpers
{
    public static class DbInit
    {
        public static void SeedProduct(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var settings = serviceScope.ServiceProvider.GetService<IOptions<MongoDbSettings>>().Value;
            var context = new ReadingIsGoodContext(settings);

            var count = context.MongoClient.GetDatabase("ReadingIsGoodDb").GetCollection<Product>("Products").CountDocuments(new BsonDocument());
            if (count > 0)
            {
                return;
            }

            var products = new List<Product>
            {
                new Product { Name="Product One", Price=15,Quantity=3},
                new Product { Name="Product Two", Price=10,Quantity=4},
                new Product { Name="Product Three",Price=25,Quantity=5},
                new Product { Name="Product Four",Price=18,Quantity=3},
                new Product { Name="Product Five",Price=45,Quantity=2},
                new Product { Name="Product Six", Price=32,Quantity=5},
                new Product { Name="Product Seven",Price=12,Quantity=7},
                new Product { Name="Product Eight",Price=100,Quantity=4},
                new Product { Name="Product Nine",Price=50,Quantity=3},
                new Product { Name="Product Ten",Price=15,Quantity=2},
                new Product { Name="Product Eleven",Price=12,Quantity=2},
                new Product { Name="Product Twelve",Price=45,Quantity=6},
                new Product { Name="Product Thirteen",Price=35,Quantity=1},
                new Product { Name="Product Fourteen",Price=20,Quantity=3},
                new Product { Name="Product Fifteen",Price=53,Quantity=2},
            };

            context.Products.InsertMany(products);
        }
    }
}
