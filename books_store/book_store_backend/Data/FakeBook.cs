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
            Description = @"George Orwell's masterpiece 1984—the title
derived by reversing the last digits of the year of its completion—is
a dystopian novel depicting an oligarchical, collectivist society.
Winston Smith, the protagonist, practices ""thoughtcrime""—he 
lets his mind wander in ways the government would disapprove 
of—and it is through him that we discover the atrocities of the society.
Orwell wrote this novel after he wrote Animal Farm; both works wanted to
depict the downfalls of a Communist regime. 1984 has been particularly
influential, and one of its creations, ""Big Brother,"" has found a 
prominent place in pop culture. Ironically, the book has, at times,
been challenged for being intellectually dangerous, even to the point 
of being banned. Its influence, however, remains unmatched and its message unforgotten.";
            Id = id;
            Image = "https://images-na.ssl-images-amazon.com/images/I/51hcQNd6RDL.jpg";
            ISBN10 = "1291296840";
            Pages = 144;
            Price = 14.75;
            Rating = 4;
            Title = "1984";
            Votes = 7368;
        }
    }
}