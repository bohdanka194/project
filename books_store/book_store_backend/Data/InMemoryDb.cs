namespace Internal
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public class InMemoryDb : CurrentContext
    {
        public InMemoryDb() : base(new DbContextOptionsBuilder<CurrentContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .EnableSensitiveDataLogging()
                  .Options)
        {
        }
    }
}
