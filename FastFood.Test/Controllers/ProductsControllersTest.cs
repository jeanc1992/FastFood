using FastFood.Domain.Dto;
using FastFood.Domain.Dto.Products.Request;
using FastFood.Domain.Dto.Products.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace FastFood.Test.Controllers
{
    internal class ProductsControllersTest
    {
        [Test]
        public async Task Create_ShouldReturn200Status()
        {
            var application = new WebApplicationFactory<Program>();

            var client = application.CreateClient();

            var response = await client.PostAsJsonAsync("/api/Products", new ProductRequestDto
            {
                Description="test product",
                Name = "test product",
                Stock = 10
            });

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponseDto<ProductResponseDto>>();
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Succeed);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            var application = new WebApplicationFactory<Program>();

            var client = application.CreateClient();

            var response = await client.GetFromJsonAsync<ProductResponseDto>("/api/Products");
        }
    }
}
