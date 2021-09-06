using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Data
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database;

        public IClientSession Session { get; set; }

        public MongoContext(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration["NoSQL-ConnectionString"]);
            _database = mongoClient.GetDatabase(configuration["NoSQL-DatabaseName"]);
        }

        public IMongoCollection<T> Collection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        public IMongoCollection<T> Collection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
