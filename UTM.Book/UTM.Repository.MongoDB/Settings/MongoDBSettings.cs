using Microsoft.Extensions.Options;

namespace UTM.Repository.MongoDB.Settings
{
	/// <summary>
	/// Settings used to connect to the MongoDB instance and database
	/// </summary>
	public interface IMongoDBSettings
	{
		/// <summary>
		/// Gets the connection string of the MongoDB instance
		/// </summary>
		string? ConnectionString { get; }
		/// <summary>
		/// Gets the MongoDB database name
		/// </summary>
		string? Database { get; }
	}

	/// <inheritdoc/>
	public class MongoDBSettings : IMongoDBSettings
	{
		private readonly IOptions<MongoDBOptions> _options;

		public MongoDBSettings(IOptions<MongoDBOptions> options)
		{
			_options = options;
		}

		/// <inheritdoc/>
		public string? ConnectionString => _options.Value.ConnectionString;

		/// <inheritdoc/>
		public string? Database => _options.Value.Database;
	}
}
