﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ReadingIsGood.API.Application.Customers.Queries
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        [Required]
        [FromRoute(Name = "customerId")]
        public string CustomerId { get; set; }
    }
}
