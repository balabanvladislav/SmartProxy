using AutoMapper;
using UTM.Domain.Models;
using UTM.Repository.MongoDB;

namespace UTM.Service
{
    public interface IBookService
    {
        public Task<List<Book>> GetAllBooks();
        public Task<BookOut> GetBookById(Guid Id);
        public Task<Book> AddBook(BookIn book);
        public Task UpsertBook(Book book);
        public Task DeleteBook(Guid id);
        public Task DeleteAll();
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

        public async Task<List<Book>> GetAllBooks()
        {
            return await _repository.GetAll();
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
        
        public async Task UpsertBook(Book book)
        {
            book.LastChangedAt = DateTime.UtcNow;
            await _repository.UpsertRecord(book);
        }

        public async Task DeleteAll()
        {
            await _repository.DeleteAll();
        }
    }
}