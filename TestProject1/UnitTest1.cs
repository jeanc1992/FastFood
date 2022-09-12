using FastFood.Domain.Dto.Products.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
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