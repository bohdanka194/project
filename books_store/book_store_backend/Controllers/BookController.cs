using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace books
{
    public class RequestBody<T1>
    {
        public RequestBody(T1 payload)
        {
            Payload = payload;
        }

        public T1 Payload { get; set; }
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

        public BookController(CurrentContext currentContext) : this(currentContext, new DbCart(currentContext, sample_user))
        {

        }

        public BookController(CurrentContext currentContext, ICart cart)
        {
            db = new DbBooks(currentContext);
            this.cart = cart;
            client = sample_user;
            this.currentContext = currentContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await db.FindAsync());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveBook(Guid id)
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
        public async Task<IActionResult> Contents()
        {
            return Ok(await cart.Contents());
        }

        [HttpGet]
        [Authorize]
        [Route("/api/cart/history")]
        public async Task<IActionResult> Payments()
        {
            return Ok(await currentContext.Payments.AsNoTracking().ToListAsync());
        }

        [HttpPost]
        [Authorize]
        [Route("/api/cart/")]
        public async Task<IActionResult> AddToCart(Guid item, int quantity)
        {
            if (quantity == 0)
            {
                return BadRequest();
            }
            await cart.Put(item, quantity);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("/api/cart/")]
        public async Task<IActionResult> RemoveFromCart(Guid item)
        {
            await cart.Extract(item);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("/api/cart/order")]
        public async Task<IActionResult> Checkout()
        {
            await cart.Submit();
            return Ok("Your order is being processed");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestBody<IBook[]> books)
        {
            if (client != sample_user)
            {
                return StatusCode(403);
            }
            await db.Add(books.Payload.Select(book => new Book(book, book.Id)));
            return Ok();
        }
    }
}
