using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using hp_proj_1_backend.Data;
using hp_proj_1_backend.Dtos.UserDto;
using hp_proj_1_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hp_proj_1_backend.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetUserDetailsDto>>> DeleteUser(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDetailsDto>>();
            try
            {
                User user = await _context.Users
                     .FirstOrDefaultAsync(c => c.ID == id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Users
                        // .Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetUserDetailsDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDetailsDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDetailsDto>>();
            var dbUsers = await _context.Users
            // .Include(c => c.AppliedJobs)
           
            .ToListAsync(); 
            serviceResponse.Data = dbUsers.Select(c => _mapper.Map<GetUserDetailsDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDetailsDto>> GetUsersById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDetailsDto>();
            var dbUser = await _context.Users
            .Include(c => c.AppliedJobs)
                 .FirstOrDefaultAsync(c => c.ID == id);

            serviceResponse.Data = _mapper.Map<GetUserDetailsDto>(dbUser);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDetailsDto>> UpdateUser(UpdateUserDetailsDto updatedUser)
        {
             var serviceResponse = new ServiceResponse<GetUserDetailsDto>();
            try
            {
                User user = await _context.Users
                    .FirstOrDefaultAsync(c => c.ID == updatedUser.ID);
                if (user!=null)
                {
                    user.FirstName = updatedUser.FirstName;
                    user.LastName = updatedUser.LastName;
                    user.ContactNum = updatedUser.ContactNum;
                    user.WorkExperience = updatedUser.WorkExperience;
                    user.Skills = updatedUser.Skills;
                    user.ImgUrl = updatedUser.ImgUrl;
                    user.Bio = updatedUser.Bio;
                    user.Email = updatedUser.Email;
                    user.Gender = updatedUser.Gender;

                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetUserDetailsDto>(user);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}