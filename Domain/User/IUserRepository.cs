using System.Threading.Tasks;

namespace Domain.User
{
    public interface IUserRepository
    {
        Task CreateUser(Model.User user);

        Model.User GetUser(string name);

        Model.User Login(string name, string password);
    }
}
