namespace Internal
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using books.Data;
    using Microsoft.EntityFrameworkCore;

    public class DbDashboard : IDashboard
    {
        private CurrentContext dbContext;
        private Guid client;

        public DbDashboard(CurrentContext dbContext, Guid client)
        {
            this.dbContext = dbContext;
            this.client = client;
        }

        public async Task Put(Guid item, int quantity = 1)
        {
            await dbContext.Database.ExecuteSqlCommandAsync(
                "exec update_cart @user={0},@item={1},@quantity={2}",
                client, item, quantity
            );
        }

        public async Task Extract(Guid item)
        {
            await dbContext.Database.ExecuteSqlCommandAsync(
                "delete from dbo.cart where Client={0} and ProductId={1}",
                client, item
            );
        }

        public async Task<List<DashoboardItem>> Contents()
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
}