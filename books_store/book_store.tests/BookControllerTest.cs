namespace book_store.tests
{
    using books;
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
            BookController books = new BookController(new InMemoryDb());
            Assert.IsAssignableFrom<OkObjectResult>(await books.GetBooks());
        }

        [Fact]
        public async Task Should_create_book()
        {
            BookController books = new BookController(new InMemoryDb());
            Assert.IsAssignableFrom<OkResult>(
                await books.Create(new RequestBody<IBook[]>(
                    new IBook[] { new FakeBook() }
                )
            ));
        }

        [Fact]
        public async Task Should_remove_book()
        {
            BookController books = new BookController(new InMemoryDb());
            Guid guid = Guid.NewGuid();
            await books.Create(new RequestBody<IBook[]>(
                new IBook[] { new FakeBook(guid) }
            ));
            Assert.IsAssignableFrom<OkResult>(
               await books.RemoveBook(guid)
            );
        }

        [Theory]
        [InlineData(5, typeof(OkResult))]
        [InlineData(1, typeof(OkResult))]
        [InlineData(0, typeof(BadRequestResult))]
        public async Task Should_add_to_cart(int quantity, Type expectedResult)
        {
            BookController books = new BookController(new InMemoryDb(), new FakeCart());
            Guid guid = Guid.NewGuid();
            await books.Create(new RequestBody<IBook[]>(
                new IBook[] { new FakeBook(guid) }
            ));
            Assert.IsAssignableFrom(
               expectedResult,
               await books.AddToCart(guid, quantity)
            );
        }

        [Fact]
        public async Task Should_remove_from_cart()
        {
            BookController books = new BookController(new InMemoryDb(), new FakeCart());
            Guid guid = Guid.NewGuid();
            await books.Create(new RequestBody<IBook[]>(
                new IBook[] { new FakeBook(guid) }
            ));
            await books.AddToCart(guid, 1);
            Assert.IsAssignableFrom<OkResult>(
               await books.RemoveFromCart(guid)
            );
        }

        [Fact]
        public async Task Should_checkout()
        {
            BookController books = new BookController(new InMemoryDb(), new FakeCart());
            Assert.IsAssignableFrom<OkObjectResult>(await books.Checkout());
        }

        [Fact]
        public async Task Should_return_cart_contents()
        {
            BookController books = new BookController(new InMemoryDb(), new FakeCart());
            Assert.IsAssignableFrom<OkObjectResult>(await books.Contents());
        }

        [Fact]
        public async Task Should_return_paid_books()
        {
            BookController books = new BookController(new InMemoryDb(), new FakeCart());
            Assert.IsAssignableFrom<OkObjectResult>(await books.Payments());
        }
    }
}