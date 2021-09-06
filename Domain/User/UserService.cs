using System.Threading.Tasks;

namespace Domain.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task CreateUser(Model.User user)
        {
            return _userRepository.CreateUser(user);
        }

        public Task<Model.User> GetUser(string name)
        {
            return _userRepository.GetUser(name);
        }
    }
}
