namespace book_store.tests
{
    using books;
    using books.Data;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class BookControllerTest
    {
        public BookControllerTest()
        {

        }

        [Fact]
        public async Task Should_have_books()
        {
            BookController books = new BookController(new InMemoryDb(), new FakeDashboard());
            Assert.IsAssignableFrom<OkObjectResult>(await books.GetBooks());
        }

        [Fact]
        public async Task Should_create_book()
        {
            BookController books = new BookController(new InMemoryDb(), new FakeDashboard());
            Assert.IsAssignableFrom<OkResult>(
                await books.Create(new FakeBook())
            );
        }

        [Fact]
        public async Task Should_remove_book()
        {
            BookController books = new BookController(new InMemoryDb(), new FakeDashboard());
            Guid guid = Guid.NewGuid();
            await books.Create(new FakeBook(guid));
            Assert.IsAssignableFrom<OkResult>(
               await books.RemoveBook(guid)
            );
        }

        [Theory]
        [InlineData(5, typeof(OkResult))]
        [InlineData(1, typeof(OkResult))]
        [InlineData(0, typeof(BadRequestResult))]
        public async Task Should_add_to_dashboard(int quantity, Type expectedResult)
        {
            BookController books = new BookController(new InMemoryDb(), new FakeDashboard());
            Guid guid = Guid.NewGuid();
            await books.Create(new FakeBook(guid));
            Assert.IsAssignableFrom(
               expectedResult,
               await books.AddToDashboard(guid, quantity)
            );
        }

        [Fact]
        public async Task Should_remove_from_dashboard()
        {
            BookController books = new BookController(new InMemoryDb(), new FakeDashboard());
            Guid guid = Guid.NewGuid();
            await books.Create(new FakeBook(guid));
            await books.AddToDashboard(guid, 1);
            Assert.IsAssignableFrom<OkResult>(
               await books.RemoveFromDashboard(guid)
            );
        }

        [Fact]
        public void Should_checkout()
        {
            BookController books = new BookController(new InMemoryDb(), new FakeDashboard());
            Assert.IsAssignableFrom<OkObjectResult>(books.Checkout());
        }

        [Fact]
        public async Task Should_return_dashboard_contents()
        {
            BookController books = new BookController(new InMemoryDb(), new FakeDashboard());
            Assert.IsAssignableFrom<OkObjectResult>(await books.Contents());
        }

        [Fact]
        public async Task Should_return_paid_books()
        {
            BookController books = new BookController(new InMemoryDb(), new FakeDashboard());
            Assert.IsAssignableFrom<OkObjectResult>(await books.Payments());
        }
    }
}