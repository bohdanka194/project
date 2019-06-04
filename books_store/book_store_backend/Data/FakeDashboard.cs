namespace books.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FakeDashboard : IDashboard
    {
        private Dictionary<Guid, DashoboardItem> dictionary = new Dictionary<Guid, DashoboardItem>();

        public Task<List<DashoboardItem>> Contents()
        {
            return Task.FromResult(new List<DashoboardItem>(dictionary.Select(pair => pair.Value)));
        }

        public Task Extract(Guid item)
        {
            dictionary.Remove(item);
            return Task.CompletedTask;
        }

        public Task Put(Guid item, int quantity)
        {
            dictionary[item] = new DashoboardItem(item, Guid.Empty, quantity);
            return Task.CompletedTask;
        }

        public Task Submit()
        {
            dictionary.Clear();
            return Task.CompletedTask;
        }
    }
}