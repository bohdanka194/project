﻿namespace books
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICart
    {
        Task Put(Guid item, int quantity);
        Task Extract(Guid item);
        Task<List<Item>> Contents();
        Task Submit();
    }
}