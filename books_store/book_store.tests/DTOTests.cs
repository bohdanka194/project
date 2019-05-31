namespace book_store.tests
{
    using books.Data;
    using System.Linq;
    using Xunit;

    public class DTOTests
    {
        [Fact] 
        public void should_create_payment_dto()
        {
            Assert.NotNull(new payment() { });
        }

        [Fact]
        public void should_copy_book()
        {
            FakeBook book1 = new FakeBook();
            var book = new Book(book1);
            Assert.True(
                new object[] {
                    book1.Author,
                    book1.Description,
                    book1.Image,
                    book1.ISBN10,
                    book1.Pages,
                    book1.Price,
                    book1.Rating,
                    book1.Title,
                    book1.Votes
                }.SequenceEqual(
                new object[] {
                    book.Author,
                    book.Description,
                    book.Image,
                    book.ISBN10,
                    book.Pages,
                    book.Price,
                    book.Rating,
                    book.Title,
                    book.Votes
                })
            );
        }
    }
}
