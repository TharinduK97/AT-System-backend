using System.Threading.Tasks;
using hp_proj_1_backend.Models;

namespace hp_proj_1_backend.Data
{
    public interface IAuthRepository
    {
          Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}