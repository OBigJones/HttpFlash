using System.Threading.Tasks;

namespace Domain.User
{
    public interface IUserService
    {
        Task CreateUser(Model.User user);

        Task<Model.User> GetUser(string name);
    }
}
