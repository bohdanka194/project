using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        static Guid sample_user = new Guid("04f85b4a-0984-46d9-a81b-af0790625aef");
        private Guid client;
        private ICart cart;
        private DbBooks db;
        private readonly CurrentContext currentContext;

        public BookController(CurrentContext currentContext)
        {
            db = new DbBooks(currentContext);
            cart = new DbCart(currentContext, sample_user);
            client = sample_user;
            this.currentContext = currentContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return await db.FindAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> RemoveBook(Guid id)
        {
            if (client != sample_user)
            {
                return StatusCode(403);
            }
            await db.Remove(id);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("/api/cart/")]
        public async Task<ActionResult<List<Item>>> Contents()
        {
            return await cart.Contents();
        }

        [HttpGet]
        [Authorize]
        [Route("/api/cart/history")]
        public async Task<ActionResult<List<payment_log>>> Payments()
        {
            return await currentContext.Payments.AsNoTracking().ToListAsync();
        }

        [HttpPost]
        [Authorize]
        [Route("/api/cart/")]
        public async Task<ActionResult> AddToCart(Guid item, int quantity)
        {
            await cart.Put(item, quantity);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("/api/cart/")]
        public async Task<ActionResult> RemoveFromCart(Guid item)
        {
            await cart.Extract(item);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("/api/cart/order")]
        public async Task<ActionResult> Checkout()
        {
            await cart.Submit();
            return Ok("Your order is being processed");
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Book book)
        {
            if (client != sample_user)
            {
                return StatusCode(403);
            } 
            await db.Add(book);
            return Ok();
        }
    }
}
