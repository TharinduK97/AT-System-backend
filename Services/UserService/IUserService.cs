using System.Collections.Generic;
using System.Threading.Tasks;
using hp_proj_1_backend.Dtos.UserDto;
using hp_proj_1_backend.Models;

namespace hp_proj_1_backend.Services.UserService
{
    public interface IUserService
    {
           Task<ServiceResponse<List<GetUserDetailsDto>>> GetAllUsers();
        Task<ServiceResponse<GetUserDetailsDto>> GetUsersById(int id);
       
        Task<ServiceResponse<GetUserDetailsDto>> UpdateUser(UpdateUserDetailsDto updatedUser);
        Task<ServiceResponse<List<GetUserDetailsDto>>> DeleteUser(int id);
    }
}