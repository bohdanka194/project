namespace books.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cart")]
    public class CartItem
    {
        public CartItem()
        {

        }
        public CartItem(Guid productId, Guid client, int quantity)
        {
            ProductId = productId;
            Client = client;
            Quantity = quantity;
        }

        public Guid ProductId { get; set; }
        [Key]
        public Guid Client { get; set; }
        public int Quantity { get; set; }
    }

    [Table("books")]
    public class Book : IBook
    {
        public Book(IBook book)
        {
            Id = book.Id;
            Author = book.Author;
            Price = book.Price;
            ISBN10 = book.ISBN10;
            Description = book.Description;
            Image = book.Image;
            Rating = book.Rating;
            Title = book.Title;
            Votes = book.Votes;
            Pages = book.Pages;
        }

        public Book()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string ISBN10 { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public short Rating { get; set; }
        public string Title { get; set; }
        public int Votes { get; set; }
        public int Pages { get; set; }
    }

    [Table("payment_history")]
    public class payment
    {
        public payment()
        {

        }

        public DateTime _When { get; set; }
        public Guid Item { get; set; }
        [Key]
        public Guid Client { get; set; }
        public int Quantity { get; set; }
    }
}