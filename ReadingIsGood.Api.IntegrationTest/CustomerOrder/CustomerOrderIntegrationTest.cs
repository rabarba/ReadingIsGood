using FluentAssertions;
using Newtonsoft.Json;
using ReadingIsGood.API.Application.CustomerOrders.Commands;
using ReadingIsGood.API.Application.CustomerOrders.Queries;
using ReadingIsGood.API.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReadingIsGood.Api.IntegrationTest.CustomerOrder
{
    public class CustomerOrderIntegrationTest
    {
        [Fact]
        public async Task Place_Customer_Order_Should_Return_BadRequest()
        {
            // Arrange
            using var httpClient = new TestClientProvider().HttpClient;

            var tokenResponse = await httpClient.PostAsync($"/api/auth/token", new StringContent("{}", Encoding.UTF8, "application/json"));
            var token = JsonConvert.DeserializeObject<HttpServiceResponseBase<string>>(await tokenResponse.Content.ReadAsStringAsync()).Data;

            var customerId = "6003ec40986bbf1afe1d5336";
            var command = new PlaceCustomerOrderCommand
            {
                Products = new System.Collections.Generic.List<ProductDto>()
            };

            var json = JsonConvert.SerializeObject(command);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.PostAsync($"/api/customers/{customerId}/orders", data);
            var customerOrderError = JsonConvert.DeserializeObject<HttpServiceResponseBase>(await response.Content.ReadAsStringAsync()).Error;

            // Assert
            customerOrderError.Exception.Should().NotBeNullOrEmpty();
            customerOrderError.Code.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Place_Customer_Order_Should_Return_CustomerOrderId()
        {
            // Arrange
            using var httpClient = new TestClientProvider().HttpClient;

            var tokenResponse = await httpClient.PostAsync($"/api/auth/token", new StringContent("{}", Encoding.UTF8, "application/json"));
            var token = JsonConvert.DeserializeObject<HttpServiceResponseBase<string>>(await tokenResponse.Content.ReadAsStringAsync()).Data;

            var customerId = "6003ec40986bbf1afe1d5336";
            var command = new PlaceCustomerOrderCommand
            {
                Products = new List<ProductDto>
                {
                    new ProductDto{Id ="6003fba51d70ba3fd5b6443b", Quantity=1},
                    new ProductDto{Id ="6003fba51d70ba3fd5b6443e", Quantity=1},
                    new ProductDto{Id ="6003fba51d70ba3fd5b64441", Quantity=1}
                }
            };

            var json = JsonConvert.SerializeObject(command);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.PostAsync($"/api/customers/{customerId}/orders", data);
            var customerOrder = JsonConvert.DeserializeObject<HttpServiceResponseBase<string>>(await response.Content.ReadAsStringAsync()).Data;

            // Assert
            customerOrder.Should().NotBeNullOrEmpty();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Get_Customer_Orders_Should_Return_Customer_Orders()
        {
            // Arrange
            using var httpClient = new TestClientProvider().HttpClient;

            var tokenResponse = await httpClient.PostAsync($"/api/auth/token", new StringContent("{}", Encoding.UTF8, "application/json"));
            var token = JsonConvert.DeserializeObject<HttpServiceResponseBase<string>>(await tokenResponse.Content.ReadAsStringAsync()).Data;
            var customerId = "6003ec40986bbf1afe1d5336";

            // Act
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.GetAsync($"/api/customers/{customerId}/orders");
            var customerOrders = JsonConvert.DeserializeObject<HttpServiceResponseBase<OrderDto>>(await response.Content.ReadAsStringAsync()).Data;

            // Assert
            customerOrders.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Customer_Order_Detail_Should_Return_Customer_Order_Detail()
        {
            // Arrange
            using var httpClient = new TestClientProvider().HttpClient;

            var tokenResponse = await httpClient.PostAsync($"/api/auth/token", new StringContent("{}", Encoding.UTF8, "application/json"));
            var token = JsonConvert.DeserializeObject<HttpServiceResponseBase<string>>(await tokenResponse.Content.ReadAsStringAsync()).Data;
            var customerId = "6003ec40986bbf1afe1d5336";
            var orderId = "6003fd74820dbcb09be1e45f";

            // Act
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.GetAsync($"/api/customers/{customerId}/orders/{orderId}");
            var customerOrderDetail = JsonConvert.DeserializeObject<HttpServiceResponseBase<OrderDetailDto>>(await response.Content.ReadAsStringAsync()).Data;

            // Assert
            customerOrderDetail.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
