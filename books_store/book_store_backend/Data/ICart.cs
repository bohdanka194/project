namespace books.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICart
    {
        Task Put(Guid item, int quantity);
        Task Extract(Guid item);
        Task<List<CartItem>> Contents();
        Task Submit();
    }
}