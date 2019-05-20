using System;

namespace books
{
    public interface IBook
    {
        string Author { get; }
        string Description { get; }
        Guid Id { get; }
        string Image { get; }
        string ISBN10 { get; }
        int Pages { get; }
        double Price { get; }
        short Rating { get; }
        string Title { get; }
        int Votes { get; }
    }
}