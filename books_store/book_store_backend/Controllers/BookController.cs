using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using books.Data;

namespace books
{
    [Route("api/books")]
    [Produces("application/json")]
    [ApiController]
    public class BookController : ControllerBase
    { 
        private ICart cart;
        private DbBooks db;
        private readonly CurrentContext currentContext;

        public BookController(CurrentContext currentContext, ICart cart)
        {
            db = new DbBooks(currentContext);
            this.cart = cart;
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
            await db.Remove(id);
            return Ok();
        }

        [HttpGet] 
        [Route("/api/cart/")]
        public async Task<IActionResult> Contents()
        {
            return Ok(await cart.Contents());
        }

        [HttpGet] 
        [Route("/api/cart/history")]
        public async Task<IActionResult> Payments()
        {
            return Ok(await currentContext.Payments.AsNoTracking().ToListAsync());
        }

        [HttpPost] 
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
        [Route("/api/cart/")]
        public async Task<IActionResult> RemoveFromCart(Guid item)
        {
            await cart.Extract(item);
            return Ok();
        }

        [HttpPost] 
        [Route("/api/cart/order")]
        public async Task<IActionResult> Checkout()
        {
            //await cart.Submit();
            return Ok("Your order is being processed");
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.Add(new Book[] { book });
            return Ok();
        }
    }
}