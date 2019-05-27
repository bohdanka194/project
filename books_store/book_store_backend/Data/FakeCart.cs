namespace books.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FakeCart : ICart
    {
        private Dictionary<Guid, Item> dictionary = new Dictionary<Guid, Item>();

        public Task<List<Item>> Contents()
        {
            return Task.FromResult(new List<Item>(dictionary.Select(pair => pair.Value)));
        }

        public Task Extract(Guid item)
        {
            dictionary.Remove(item);
            return Task.CompletedTask;
        }

        public Task Put(Guid item, int quantity)
        {
            dictionary[item] = new Item(item, Guid.Empty, quantity);
            return Task.CompletedTask;
        }

        public Task Submit()
        {
            dictionary.Clear();
            return Task.CompletedTask;
        }
    }
}