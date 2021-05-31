using System.Threading.Tasks;
using hp_proj_1_backend.Data;
using hp_proj_1_backend.Dtos.UserDto;
using hp_proj_1_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace hp_proj_1_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;

        }

        
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepo.Register(
                new User { Role = request.Role, FirstName= request.FirstName, LastName=request.LastName, ContactNum = request.ContactNum,
                WorkExperience = request.WorkExperience,Skills=request.Skills,ImgUrl=request.ImgUrl,Bio=request.Bio,Email=request.Email,Gender=request.Gender, CreatedAt= request.CreatedAt }
                , request.Password
            );

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authRepo.Login(
                request.Email, request.Password
            );

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}