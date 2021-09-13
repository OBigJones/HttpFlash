using System;
using System.Net.NetworkInformation;
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
            var mac = GetEnderecoMAC1();

            user.MacAdress = mac;
            user.State = "Active";

            return _userRepository.CreateUser(user);
        }

        public Model.User GetUser(string name)
        {
            return _userRepository.GetUser(name);
        }

        private static string GetEnderecoMAC1()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                var enderecoMAC = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    // retorna endereço MAC do primeiro cartão
                    if (enderecoMAC == string.Empty)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        enderecoMAC = adapter.GetPhysicalAddress().ToString();
                    }
                }
                return enderecoMAC;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
