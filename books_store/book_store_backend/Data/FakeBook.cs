namespace books.Data
{
    using System;

    public class FakeBook : Book
    {
        public FakeBook() : this(Guid.NewGuid())
        {

        }

        public FakeBook(Guid id)
        {
            Author = "George Orwell";
            Description = "No description.";
            Id = id;
            Image = "no";
            ISBN10 = "no";
            Pages = 552;
            Price = 14.75;
            Rating = 4;
            Title = "1984";
            Votes = 7368;
        }
    }
}