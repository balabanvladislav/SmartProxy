using UTM.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using UTM.Service;

namespace UTM.BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<List<Book>> GetAllBooks()
        {
            return await _bookService.GetAllBooks();
        }

        [HttpGet("{id}")]
        public async Task<BookOut> GetBookById(Guid id)
        {
            return await _bookService.GetBookById(id);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookIn bookIn)
        {
            var result = await _bookService.AddBook(bookIn);
            
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Upsert(Book book)
        {
            await _bookService.UpsertBook(book);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookService.DeleteBook(id);

            return Ok();
        }

        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll()
        {
            await _bookService.DeleteAll();

            return Ok();
        }
    }
}
