using Domain.User;
using Domain.User.Model;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoContext context)
        {
            _collection = context.Collection<User>();
        }

        public Task CreateUser(User user)
        {
            return _collection.InsertOneAsync(user);
        }

        public User GetUser(string name)
        {
            var filter = Builders<User>.Filter.Where(x => x.Username.Equals(name));
            return _collection.FindSync(filter).FirstOrDefault();
        }

        public User Login(string name, string password)
        {
            var filter = Builders<User>.Filter.Where(x => x.Username.Equals(name) 
            && x.Password.Equals(password) 
            && x.State.Equals("Active"));
            return _collection.FindSync(filter).FirstOrDefault();
        }
    }
}
