using FluentAssertions;
using Newtonsoft.Json;
using ReadingIsGood.API.Application.Customers.Commands;
using ReadingIsGood.API.Application.Customers.Queries;
using ReadingIsGood.API.Models;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReadingIsGood.Api.IntegrationTest.Customer
{
    public class CustomerIntegrationTest
    {
        [Fact]
        public async Task Register_Customer_Should_Return_UnAuthorized()
        {
            // Arrange
            using var httpClient = new TestClientProvider().HttpClient;
            var customer = new Domain.Documents.Customer()
            {
                Name = "Ugur",
                Address = "Merter - Istanbul",
                Phone = "506 533 53 53",
                Email = "test@gmail.com"
            };

            // Act
            var response = await httpClient.PostAsync($"/api/customers", new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Register_Customer_Should_Return_BadRequest()
        {
            // Arrange
            using var httpClient = new TestClientProvider().HttpClient;

            var tokenResponse = await httpClient.PostAsync($"/api/auth/token", new StringContent("{}", Encoding.UTF8, "application/json"));
            var token = JsonConvert.DeserializeObject<HttpServiceResponseBase<string>>(await tokenResponse.Content.ReadAsStringAsync()).Data;

            var command = new RegisterCustomerCommand
            {
                Name = "Ugur",
                Address = "Merter - Istanbul"
            };

            var json = JsonConvert.SerializeObject(command);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.PostAsync($"/api/customers", data);
            var customerError = JsonConvert.DeserializeObject<HttpServiceResponseBase>(await response.Content.ReadAsStringAsync()).Error;

            // Assert
            customerError.Exception.Should().NotBeNullOrEmpty();
            customerError.Code.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Register_Customer_Should_Return_CustomerId()
        {
            // Arrange
            using var httpClient = new TestClientProvider().HttpClient;

            var tokenResponse = await httpClient.PostAsync($"/api/auth/token", new StringContent("{}", Encoding.UTF8, "application/json"));
            var token = JsonConvert.DeserializeObject<HttpServiceResponseBase<string>>(await tokenResponse.Content.ReadAsStringAsync()).Data;

            var command = new RegisterCustomerCommand
            {
                Name = "Ugur",
                Address = "Merter - Istanbul",
                Phone = "506 533 53 53",
                Email = "test@gmail.com"
            };

            var json = JsonConvert.SerializeObject(command);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.PostAsync($"/api/customers", data);
            var customer = JsonConvert.DeserializeObject<HttpServiceResponseBase<string>>(await response.Content.ReadAsStringAsync()).Data;

            // Assert
            customer.Should().NotBeNullOrEmpty();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Get_Customer_Should_Return_Customer()
        {
            // Arrange
            using var httpClient = new TestClientProvider().HttpClient;

            var tokenResponse = await httpClient.PostAsync($"/api/auth/token", new StringContent("{}", Encoding.UTF8, "application/json"));
            var token = JsonConvert.DeserializeObject<HttpServiceResponseBase<string>>(await tokenResponse.Content.ReadAsStringAsync()).Data;
            var customerId = "6003ec40986bbf1afe1d5336";

            // Act
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.GetAsync($"/api/customers/{customerId}");
            var customer = JsonConvert.DeserializeObject<HttpServiceResponseBase<CustomerDto>>(await response.Content.ReadAsStringAsync()).Data;

            // Assert
            customer.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Customer_Should_Return_BadRequest()
        {
            // Arrange
            using var httpClient = new TestClientProvider().HttpClient;

            var tokenResponse = await httpClient.PostAsync($"/api/auth/token", new StringContent("{}", Encoding.UTF8, "application/json"));
            var token = JsonConvert.DeserializeObject<HttpServiceResponseBase<string>>(await tokenResponse.Content.ReadAsStringAsync()).Data;
            var customerId = "fdslşf";

            // Act
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await httpClient.GetAsync($"/api/customers/{customerId}");
            var customerError = JsonConvert.DeserializeObject<HttpServiceResponseBase>(await response.Content.ReadAsStringAsync()).Error;

            // Assert
            customerError.Should().NotBeNull();
            customerError.Code.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
