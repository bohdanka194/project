namespace books
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class DbBooks
    {
        private CurrentContext dbContext;

        public DbBooks(CurrentContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Add(IEnumerable<Book> books)
        {
            dbContext.Set<Book>().AddRange(books);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Book>> FindAsync()
        {
            return await dbContext.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book> GetAsync(Guid aggregateId)
        {
            return await dbContext.Books.FindAsync(aggregateId).ConfigureAwait(false);
        }

        public async Task Remove(Guid aggregateId)
        {
            Book entity = await GetAsync(aggregateId);
            dbContext.Books.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}