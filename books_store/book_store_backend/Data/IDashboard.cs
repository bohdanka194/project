namespace books.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDashboard
    {
        Task Put(Guid item, int quantity);
        Task Extract(Guid item);
        Task<List<DashoboardItem>> Contents();
        Task Submit();
    }
}