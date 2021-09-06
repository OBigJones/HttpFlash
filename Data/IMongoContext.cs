using MongoDB.Driver;

namespace Data
{
    public interface IMongoContext
    {
        IMongoCollection<T> Collection<T>();

        IMongoCollection<T> Collection<T>(string collectionName);
    }
}
