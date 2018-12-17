using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;


namespace books
{ 
    public class post_params<T1,T2,T3>
    {
        public T1 item1 { get; set; }
        public T2 item2 { get; set; }
        public T3 item3 { get; set; }
    }

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private SqlServerRepo db;

        public BookController(CurrentContext currentContext)
        {
            db = new SqlServerRepo(currentContext);
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return await db.FindAsync();
        }

        [HttpPost] 
        public async Task<ActionResult> Create([FromBody] Book book)
        {
            await db.Add(book);
            return Ok();
        }

    }
}
