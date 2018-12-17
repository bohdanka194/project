namespace books
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class SqlServerRepo 
    {
        private CurrentContext dbContext;

        public SqlServerRepo(CurrentContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Add(Book book)
        {
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Book>> FindAsync()
        {
            return await dbContext.Books.ToListAsync();
        }

        public async Task<Book> GetAsync(Guid aggregateId)
        {
            return await dbContext.Books.FromSql(
               "select Id,Title,Author,Price from dbo.books where Id={0}",
               aggregateId
           ).FirstAsync().ConfigureAwait(false);
        }

        public async Task Remove(Guid aggregateId)
        {
            await dbContext.Database.ExecuteSqlCommandAsync(
                "delete from dbo.books where Id={0}", 
                aggregateId
            );
        }
    }
}