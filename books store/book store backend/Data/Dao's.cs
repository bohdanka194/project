﻿namespace books
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cart")]
    public class Item 
    {
        public Item()
        {

        }
        public Item(Guid productId, Guid client, int quantity)
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
    public class Book
    {
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
}