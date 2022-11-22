using Microsoft.Extensions.Options;

namespace UTM.Service.Settigs
{
    public interface ISyncServiceSettings
    {
        /// <summary>
        /// Host
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Upsert
        /// </summary>
        public string UpsertHttpMethod { get; }

        /// <summary>
        /// Delete
        /// </summary>
        public string DeleteHttpMethod { get; }

    }
    public class SyncServiceSettings : ISyncServiceSettings
    {
        private readonly IOptions<SyncServiceOptions> _options;
        public SyncServiceSettings(IOptions<SyncServiceOptions> options)
        {
            _options = options;
        }

        /// <inheritdoc/>
        public string Host => _options.Value.Host;

        /// <inheritdoc/>
        public string UpsertHttpMethod => _options.Value.UpsertHttpMethod;

        /// <inheritdoc/>
        public string DeleteHttpMethod => _options.Value.DeleteHttpMethod;
    }
}
