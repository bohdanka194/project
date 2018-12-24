namespace books
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface ICart
    {
        Task Put(Guid item, int quantity);
        Task Extract(Guid item);
        Task<List<Item>> Contents();
        Task Submit();
    }

    public class DbCart : ICart
    {
        private CurrentContext dbContext;
        private Guid client;

        public DbCart(CurrentContext dbContext, Guid client)
        {
            this.dbContext = dbContext;
            this.client = client;
        }

        public async Task Put(Guid item, int quantity = 1)
        {
            dbContext.Cart.Add(new Item(item, client, quantity));
            await dbContext.SaveChangesAsync();
        }

        public async Task Extract(Guid item)
        {
            await dbContext.Database.ExecuteSqlCommandAsync(
                "delete from dbo.cart where Client={0} and ProductId={1}",
                client, item
            );
        }

        public async Task<List<Item>> Contents()
        {
            return await dbContext.Cart.AsNoTracking().ToListAsync();
        }

        public async Task Submit()
        {
            await dbContext.Database.ExecuteSqlCommandAsync(
               @"exec log_payment_details @Client={0}",
               client
            );
        }
    }

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
            return await dbContext.Books.AsNoTracking().ToListAsync();
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