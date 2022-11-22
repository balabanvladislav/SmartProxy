using UTM.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using UTM.Service;
using UTM.BookAPI.Services;
using AutoMapper;

namespace UTM.BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ISyncService<Book> _syncService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, ISyncService<Book> syncService, IMapper mapper)
        {
            _bookService = bookService;
            _syncService = syncService;
            _mapper = mapper;
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

            await _syncService.Upsert(result);
            
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Upsert(Book book)
        {
            var result = await _bookService.UpsertBook(book);

            await _syncService.Upsert(result);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var bookOut = await _bookService.GetBookById(id);

            if(bookOut == null)
            {
                return NotFound();
            }

            var book = _mapper.Map<Book>(bookOut);
            book.LastChangedAt = DateTime.UtcNow;

            await _bookService.DeleteBook(id);

            await _syncService.Delete(book);
            
            return Ok("Deleted " + id);
        }

        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll()
        {
            await _bookService.DeleteAll();

            return Ok();
        }

        [HttpPut("sync")]
        public async Task<IActionResult> UpsertSync(Book book)
        {
            var existingBook = await _bookService.GetBookById(book.Id);

            if(existingBook == null || existingBook.LastChangedAt < book.LastChangedAt)
            {
                await _bookService.UpsertBook(book);
            }
            return Ok();
        }

        [HttpDelete("sync")]
        public async Task<IActionResult> DeleteSync(Book book)
        {
            var existingBook = await _bookService.GetBookById(book.Id);

            if (existingBook != null || existingBook.LastChangedAt > book.LastChangedAt)
            {
                await _bookService.DeleteBook(book.Id);
            }
            return Ok();
        }

        [HttpGet("test")]
        public async Task Test()
        {
            await _bookService.Test();
        }
    }
}
