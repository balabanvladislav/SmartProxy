using UTM.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace UTM.BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        [HttpGet("book")]
        public List<Book> GetAllBooks()
        {
            return new List<Book>();
        }
    }
}
