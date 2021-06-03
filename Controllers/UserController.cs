using System.Collections.Generic;
using System.Threading.Tasks;
using hp_proj_1_backend.Dtos.UserDto;
using hp_proj_1_backend.Models;
using hp_proj_1_backend.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hp_proj_1_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;

        }


        // [Authorize(Roles = "Admin")]
         [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDetailsDto>>>> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        //  [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDetailsDto>>> GetSingle(int id)
        {
            return Ok(await _userService.GetUsersById(id));
        }

        // [Authorize]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetUserDetailsDto>>> UpdateCharacter(UpdateUserDetailsDto updatedUser)
        {
            var response = await _userService.UpdateUser(updatedUser);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

         [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDetailsDto>>>> Delete(int id)
        {
            var response = await  _userService.DeleteUser(id);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}