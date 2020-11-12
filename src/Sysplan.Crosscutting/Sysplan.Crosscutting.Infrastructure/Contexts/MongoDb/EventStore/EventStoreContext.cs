using MongoDB.Driver;

namespace Sysplan.Crosscutting.Infrastructure.Contexts.MongoDb
{
    public class EventStoreContext : IEventStoreContext
    {
        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }

        public EventStoreContext(EventStoreContextConfig config, MongoClient client)
        {
            Client = client;
            Database = GetMongoDatabase(config.EventStoreDatabase);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return Database.GetCollection<T>(collectionName);
        }

        private IMongoDatabase GetMongoDatabase(string databaseName)
        {
            try
            {
                return Client.GetDatabase(databaseName);
            }
            catch
            {
                return GetMongoDatabase(databaseName);
            }
        }
    }
}