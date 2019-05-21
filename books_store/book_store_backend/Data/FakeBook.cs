namespace books
{
    using System;

    public class FakeBook : IBook
    {
        public FakeBook() : this(Guid.NewGuid())
        {

        }

        public FakeBook(Guid id)
        {
            Author = "George Orwell";
            Description = "No description.";
            Id = id;
            Image = string.Empty;
            ISBN10 = string.Empty;
            Pages = 552;
            Price = 14.75;
            Rating = 4;
            Title = "1984";
            Votes = 7368;
        }

        public string Author  { get; set; }

        public string Description  { get; set; }

        public Guid Id  { get; set; }

        public string Image  { get; set; }

        public string ISBN10  { get; set; }

        public int Pages  { get; set; }

        public double Price  { get; set; }

        public short Rating  { get; set; }

        public string Title  { get; set; }

        public int Votes  { get; set; }
    }
}