using System.Collections.Concurrent;
using System.Diagnostics;
using UTM.Domain;
using UTM.Domain.Models;
using UTM.SyncNode.Settings;

namespace UTM.SyncNode.Services
{
    public interface ISyncWorkJobService
    {

    }
    public class SyncWorkJobService : IHostedService
    {
        private readonly ConcurrentDictionary<Guid, SyncEntity> documents = new();
        private Timer _timer;
        private readonly IBookAPISettings _settings;

        public SyncWorkJobService(IBookAPISettings bookAPISettings)
        {
            _settings = bookAPISettings;
        }

        public void AddItem(SyncEntity entity)
        {
            SyncEntity document = null;
            bool isPresent = documents.TryGetValue(entity.Id, out document);

            if (!isPresent || (isPresent && entity.LastChangedAt > document!.LastChangedAt))
            {
                documents[entity.Id] = entity;
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoSendWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoSendWork(object state)
        {
            foreach(var doc in documents)
            {
                SyncEntity entity = null;
                var isPresent = documents.TryRemove(doc.Key, out entity);

                if (isPresent)
                {
                    var receivers = _settings.Hosts.Where(x => !x.Contains(entity.Origin));
                    foreach(var receiver in receivers)
                    {
                        var url = $"{receiver}/{entity.ObjectType}/sync";
                        try
                       {
                            var result = HttpClientUtility.SendJson(entity.JsonData, url, entity.SyncType);

                            if (!result.Result.IsSuccessStatusCode)
                            {
                                Debug.WriteLine("Result is not success");
                            }
                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }
                }
            }
        }
    }
}
