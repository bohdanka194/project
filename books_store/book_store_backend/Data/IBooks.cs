using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace books.Data
{
    public class BookLink
    {
        public string Link { get; set; }
        public Book Item { get; set; }
        public double TopPrice { get; set; }
        public string Note { get; set; }
    } 

    public interface IBooks
    {
        Task<BookLink> Links();
        Task Add(IEnumerable<Book> books);
        Task<List<Book>> FindAsync();
        Task<Book> GetAsync(Guid aggregateId);
        Task Remove(Guid aggregateId);
    }
}