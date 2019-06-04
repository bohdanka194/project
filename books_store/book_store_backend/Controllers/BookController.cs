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
        private IDashboard dashboard;
        private DbBooks db;
        private readonly CurrentContext currentContext;

        public BookController(CurrentContext currentContext, IDashboard dashboard)
        {
            db = new DbBooks(currentContext);
            this.dashboard = dashboard;
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
        [Route("/api/dashboard/")]
        public async Task<IActionResult> Contents()
        {
            return Ok(await dashboard.Contents());
        }

        [HttpGet] 
        [Route("/api/dashboard/history")]
        public async Task<IActionResult> Payments()
        {
            return Ok(await currentContext.Payments.AsNoTracking().ToListAsync());
        }

        [HttpPost] 
        [Route("/api/dashboard/")]
        public async Task<IActionResult> AddToDashboard(Guid item, int quantity)
        {
            if (quantity == 0)
            {
                return BadRequest();
            }
            await dashboard.Put(item, quantity);
            return Ok();
        }

        [HttpDelete] 
        [Route("/api/dashboard/")]
        public async Task<IActionResult> RemoveFromDashboard(Guid item)
        {
            await dashboard.Extract(item);
            return Ok();
        }

        [HttpPost] 
        [Route("/api/dashboard/order")]
        public IActionResult Checkout()
        {
            //await dashboard.Submit();
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