using MongoDB.Driver;

namespace Sysplan.Crosscutting.Infrastructure.Contexts.MongoDb
{
    public interface IEventStoreContext
    {
        IMongoDatabase Database { get; }
        IMongoCollection<T> GetCollection<T>(string collectionName);
        IMongoClient Client { get; }
    }
}