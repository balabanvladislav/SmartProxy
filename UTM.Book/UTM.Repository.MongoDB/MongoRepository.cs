using MongoDB.Bson;
using MongoDB.Driver;
using UTM.Repository.MongoDB.Models;
using UTM.Repository.MongoDB.Settings;

namespace UTM.Repository.MongoDB
{
    public interface IMongoRepository<T> where T : MongoDocument
    {
        Task<List<T>> GetAll();
        Task<T> InsertRecord(T record);
        Task<T> GetRecordById(Guid id);
        Task UpsertRecord(T record);
        Task DeleteRecordById(Guid id);
        Task DeleteAll();
    }

    public class MongoRepository<T> : IMongoRepository<T> where T: MongoDocument
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<T> _collection;
        public MongoRepository(IMongoDBSettings mongoDBSettings)
        {
            _db = new MongoClient(mongoDBSettings.ConnectionString).GetDatabase(mongoDBSettings.Database);

            var tableName = typeof(T).Name.ToLower();
            _collection = _db.GetCollection<T>(tableName);
        }

        public async Task DeleteRecordById(Guid Id)
        {
            await _collection.DeleteOneAsync(x => x.Id == Id);
        }

        public async Task<List<T>> GetAll()
        {
            var records = await _collection.FindAsync(new BsonDocument());

            return await records.ToListAsync();
        }

        public async Task<T> GetRecordById(Guid Id)
        {
            var record = await _collection.FindAsync(x => x.Id == Id);


            return await record.FirstOrDefaultAsync();
        }

        public async Task<T> InsertRecord(T record)
        {
            await _collection.InsertOneAsync(record);

            return record;
        }

        public async Task UpsertRecord(T record)
        {
            var result = await _collection.ReplaceOneAsync(d => d.Id == record.Id, record,
                new ReplaceOptions() { IsUpsert = true});
        }
        public async Task DeleteAll()
        {
            await _collection.DeleteManyAsync(new BsonDocument());
        }

    }
}
