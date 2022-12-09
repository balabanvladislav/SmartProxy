using AutoMapper;
using UTM.Domain.Models;
using UTM.Repository.MongoDB;

namespace UTM.Service
{
    public interface IBookService
    {
        public Task<List<BookIn>> GetAllBooks();
        public Task<BookOut> GetBookById(Guid Id);
        public Task<Book> AddBook(BookIn book);
        public Task<Book> UpsertBook(Book book);
        public Task DeleteBook(Guid id);
        public Task DeleteAll();

        public Task Test();
    }
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;

        private readonly IMongoRepository<Book> _repository;

        public BookService(IMongoRepository<Book> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Book> AddBook(BookIn bookIn)
        {
            var book = _mapper.Map<Book>(bookIn);
            book.LastChangedAt = DateTime.UtcNow;

            return await _repository.InsertRecord(book);
        }

        public async Task<List<BookIn>> GetAllBooks()
        {
            List<Book> books = await _repository.GetAll();

            IEnumerable<BookOut> booksOut = _mapper.Map<List<Book>, IEnumerable<BookOut>>(books);

            List<BookIn> bookInList = _mapper.Map<List<BookIn>>(booksOut);

            return bookInList;
        }

        public async Task DeleteBook(Guid Id)
        {
            await _repository.DeleteRecordById(Id);
        }

        public async Task<BookOut> GetBookById(Guid Id)
        {
            var record = await _repository.GetRecordById(Id);
            return _mapper.Map<BookOut>(record);
        }
        
        public async Task<Book> UpsertBook(Book book)
        {
            book.LastChangedAt = DateTime.UtcNow;

            await _repository.UpsertRecord(book);

            return book;
        }

        public async Task DeleteAll()
        {
            await _repository.DeleteAll();
        }

        public async Task Test()
        {
            List<BookIn> booksIn = new List<BookIn>
            {
                new BookIn
                {
                    Title = "1"
                },
                new BookIn
                {
                    Title = "2"
                }
            };

            IEnumerable<BookIn> booksInEn = booksIn;
            
            var test = _mapper.Map<List<Book>>(booksInEn);

            await Task.Delay(0);
        }
    }
}