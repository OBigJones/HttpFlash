using System.Threading.Tasks;

namespace Domain.User
{
    public interface IUserRepository
    {
        Task CreateUser(Model.User user);

        Task<Model.User> GetUser(string name);
    }
}
