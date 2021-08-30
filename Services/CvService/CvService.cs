using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using hp_proj_1_backend.Data;
using hp_proj_1_backend.Dtos.UserDto;
using hp_proj_1_backend.Models;
using hp_proj_1_backend_master.Dtos.CvDto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hp_proj_1_backend_master.Services.CvService
{
    public class CvService : ICvService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CvService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;

        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);


        public async Task<ServiceResponse<GetUserDetailsDto>> AddCv(AddCvDto newCv)
        {
          //  var response = new ServiceResponse<List<GetCvDto>>();
        //     Cv cv = _mapper.Map<Cv>(newCv);
        //          cv.User= await _context.Users.FirstOrDefaultAsync(u => u.ID == GetUserId());
                
        //     _context.Cvs.Add(cv);
        //     await _context.SaveChangesAsync();
        //     serviceResponse.Data = await _context.Cvs
        //          .Where(c => c.User.ID == GetUserId())
        //         .Select(c => _mapper.Map<GetCvDto>(c)).ToListAsync();
        //     return serviceResponse;


            var response = new ServiceResponse<GetUserDetailsDto>();
            try
            {
                
                 Cv cv = _mapper.Map<Cv>(newCv);
                 User user= await _context.Users.FirstOrDefaultAsync(u => u.ID == GetUserId());
                 cv.UserID=user.ID;
                 if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                _context.Cvs.Add(cv);
                await _context.SaveChangesAsync();
                
                 response.Data = _mapper.Map<GetUserDetailsDto>(user);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        
            
        }

        public async Task<ServiceResponse<GetCvDto>> GetCvsById()
        {
            
             var serviceResponse = new ServiceResponse<GetCvDto>();
            // var dbCv = await _context.Cvs

            //      .FirstOrDefaultAsync(c => c.UserID == GetUserId());

            // serviceResponse.Data = _mapper.Map<GetCvDto>(dbCv);
            // return serviceResponse;

            try
            {
                
                  var dbCv = await _context.Cvs

                 .FirstOrDefaultAsync(c => c.UserID == GetUserId());

            serviceResponse.Data = _mapper.Map<GetCvDto>(dbCv);
                
                 
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCvDto>> UpdateCv(UpdateCvDto updatedCv)
        {
             var serviceResponse = new ServiceResponse<GetCvDto>();
            try
            {
                Cv cv = await _context.Cvs
                     .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.UserID == GetUserId());
                if (cv.User.Role == GetUserRole())
                {
                    cv.Cvpath = updatedCv.Cvpath;
                    
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetCvDto>(cv);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Cv not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
 
        }

        public async Task<ServiceResponse<GetCvDto>> GetUserCvsById(int id)
        {
           var serviceResponse = new ServiceResponse<GetCvDto>();
            // var dbCv = await _context.Cvs

            //      .FirstOrDefaultAsync(c => c.UserID == GetUserId());

            // serviceResponse.Data = _mapper.Map<GetCvDto>(dbCv);
            // return serviceResponse;

            try
            {
                
                  var dbCv = await _context.Cvs

                 .FirstOrDefaultAsync(c => c.UserID == id);

            serviceResponse.Data = _mapper.Map<GetCvDto>(dbCv);
                
                 
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