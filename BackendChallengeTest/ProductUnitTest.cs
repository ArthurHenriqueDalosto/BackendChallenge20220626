using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using ChallangeData.Model.Product;
using BackendChallenge;

namespace BackendChallengeTest
{
    public class Tests
    {
        [Test]
        public async Task GetMessage()
        {
            using var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();
            var activity = await client.GetStringAsync("/");
            Assert.IsNotEmpty(activity);
        }

        [Test]
        public async Task FindByCodeTest()
        {
            using var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();
            var activity = await client.GetFromJsonAsync<Product>("/products/code");
            Assert.IsNotNull(activity);
        }

        [Test]
        public async Task FindAllTest()
        {
            using var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();
            var activity = await client.GetFromJsonAsync<List<Product>>("/products");
            Assert.IsNotNull(activity);
        }
    }
}