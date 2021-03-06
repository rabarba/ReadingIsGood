﻿using MongoDB.Bson;
using ReadingIsGood.Domain.Documents;
using System.Threading.Tasks;

namespace ReadingIsGood.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Create a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<string> CreateCustomerAsync(Customer customer);

        /// <summary>
        /// Get a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Customer> GetCustomerAsync(string id);
    }
}
