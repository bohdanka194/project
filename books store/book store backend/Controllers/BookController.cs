using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;


namespace books
{
    public class post_params<T1, T2, T3>
    {
        public T1 item1 { get; set; }
        public T2 item2 { get; set; }
        public T3 item3 { get; set; }
    }

    [Route("api/books")]
    [Produces("application/json")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private ICart cart;
        private SqlServerRepo db;

        public BookController(CurrentContext currentContext, ICart cart)
        {
            db = new SqlServerRepo(currentContext);
            cart = new DbCart(currentContext, Guid.NewGuid());
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return await db.FindAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task RemoveBook(Guid id)
        {
            await db.Remove(id);
        }

        [HttpPost]
        [Route("/api/cart/{item}")]
        public async Task AddToCart(Guid item, [FromBody] int quantity)
        {
            await cart.Put(item, quantity);
        }

        [HttpPost]
        [Route("/api/cart/{item}")]
        public async Task RemoveFromCart(Guid item)
        {
            await cart.Extract(item);
        }

        [HttpPost]
        [Route("/api/cart/order")]
        public async Task<ActionResult> Checkout(Guid item, [FromBody] int quantity)
        {
            await Task.Delay(TimeSpan.FromSeconds(15));
            await cart.Submit();
            return Ok("Your order is being processed");
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Book book)
        {
            await db.Add(book);
            return Ok();
        }

    }
}
