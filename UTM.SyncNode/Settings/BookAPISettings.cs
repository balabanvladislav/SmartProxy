using Microsoft.Extensions.Options;

namespace UTM.SyncNode.Settings
{
    public interface IBookAPISettings
    {
        /// <summary>
        /// Hosts
        /// </summary>
        public string[] Hosts { get; }
    }
    public class BookAPISettings : IBookAPISettings
    {
        private readonly IOptions<BookAPIOptions> _options;

        public BookAPISettings(IOptions<BookAPIOptions> options)
        {
            _options = options;
        }

        /// <inheritdoc/>
        public string[] Hosts => _options.Value.Hosts;
    }
}
