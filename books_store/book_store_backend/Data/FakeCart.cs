namespace books.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FakeCart : ICart
    {
        private Dictionary<Guid, CartItem> dictionary = new Dictionary<Guid, CartItem>();

        public Task<List<CartItem>> Contents()
        {
            return Task.FromResult(new List<CartItem>(dictionary.Select(pair => pair.Value)));
        }

        public Task Extract(Guid item)
        {
            dictionary.Remove(item);
            return Task.CompletedTask;
        }

        public Task Put(Guid item, int quantity)
        {
            dictionary[item] = new CartItem(item, Guid.Empty, quantity);
            return Task.CompletedTask;
        }

        public Task Submit()
        {
            dictionary.Clear();
            return Task.CompletedTask;
        }
    }
}