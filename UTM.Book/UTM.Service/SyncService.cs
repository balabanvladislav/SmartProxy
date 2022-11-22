using Microsoft.AspNetCore.Http;
using System.Text.Json;
using UTM.Domain;
using UTM.Domain.Models;
using UTM.Repository.MongoDB.Models;
using UTM.Service.Settigs;

namespace UTM.BookAPI.Services
{
    public interface ISyncService<T> where T : MongoDocument
    {
        Task<HttpResponseMessage> Upsert(T record);
        Task<HttpResponseMessage> Delete(T record);
    }
    public class SyncService<T> : ISyncService<T> where T : MongoDocument
    {
        private readonly ISyncServiceSettings _settings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SyncService(ISyncServiceSettings settings, IHttpContextAccessor httpContextAccessor)
        {
            _settings = settings;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<HttpResponseMessage> Delete(T record)
        {
            var syncType = _settings.DeleteHttpMethod;
            var json = ToSyncEntityJson  (record, syncType);

            var response = await HttpClientUtility.SendJson(json, _settings.Host, "POST");

            return response;
        }

        public async Task<HttpResponseMessage> Upsert(T record)
        {
            var syncType = _settings.UpsertHttpMethod;
            var json = ToSyncEntityJson(record, syncType);

            var response = await HttpClientUtility.SendJson(json, _settings.Host, "POST");

            return response;
        }

        private string ToSyncEntityJson(T record, string syncType)
        {
            var objectType = typeof(T);
            var syncEntity = new SyncEntity()
            {
                JsonData = JsonSerializer.Serialize(record),
                SyncType = syncType,
                ObjectType = objectType.Name,
                Id = record.Id,
                LastChangedAt = record.LastChangedAt,
                Origin = _httpContextAccessor!.HttpContext!.Request.Host.ToString()
            };

            var json = JsonSerializer.Serialize(syncEntity);

            return json;
        }
    }
}
