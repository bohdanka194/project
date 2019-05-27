using books;
using books.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace book_store.api_tests
{
    public class ApiTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private Lazy<HttpClient> _client;
        private ITestOutputHelper output;

        public ApiTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _client = new Lazy<HttpClient>(() =>
            {
                HttpClient client = factory.CreateClient();
                HttpResponseMessage login =
                    client.PostAsJsonAsync("/api/auth/token", new AuthModel("qwerty", "55555"))
                          .GetAwaiter()
                          .GetResult();

                string content = ReadToString(login).GetAwaiter().GetResult();

                string jwt = JObject.Parse(content)["access_token"].Value<string>();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwt);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );
                return client;
            });
            this.output = output;
        }

        [Fact]
        public async Task CanGetBooks()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.Value.GetAsync("/api/books");
            await Log(httpResponse);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(stringResponse);
            Assert.NotNull(books);
        }

        [Fact]
        public async Task CanPostBook()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.Value.PostAsJsonAsync("/api/books", new FakeBook());
            await Log(httpResponse);
            httpResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CanDeleteBook()
        {
            // The endpoint or route of the controller action.
            FakeBook value = new FakeBook();
            await _client.Value.PostAsJsonAsync("/api/books", value);

            HttpResponseMessage deleteResponse = await _client.Value.DeleteAsync("/api/books/" + value.Id.ToString());
            await Log(deleteResponse);

            deleteResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CanCheckout()
        {
            HttpResponseMessage httpResponse = await _client.Value.PostAsJsonAsync<string>("/api/cart/order", null);
            await Log(httpResponse);

            httpResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CanGetCartHistory()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.Value.GetAsync("/api/cart/history");
            await Log(httpResponse);

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(stringResponse);
            Assert.NotNull(books);
        }

        [Fact]
        public async Task CanGetCartContents()
        {
            // The endpoint or route of the controller action.
            HttpResponseMessage httpResponse;
            FakeBook value = new FakeBook();
            httpResponse = await _client.Value.PostAsJsonAsync("/api/books", value);

            await Log(httpResponse);

            httpResponse.EnsureSuccessStatusCode();

            await _client.Value.PostAsJsonAsync<string>(
                string.Format("/api/cart?item={0}&quantity={1}", value.Id, 5), null
            );
            httpResponse.EnsureSuccessStatusCode();

            await Log(httpResponse);
            httpResponse = await _client.Value.GetAsync("/api/cart");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(stringResponse);
            Assert.NotNull(books);
        }

        private async Task Log(HttpResponseMessage httpResponse)
        {
            output.WriteLine(
                await ReadToString(httpResponse)
            );
        }

        private static async Task<string> ReadToString(HttpResponseMessage httpResponse)
        {
            return await new StreamReader(
                await httpResponse.Content.ReadAsStreamAsync()
            ).ReadToEndAsync();
        }
    }
}
