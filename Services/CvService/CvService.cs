using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using hp_proj_1_backend.Data;
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


        public async Task<ServiceResponse<List<GetCvDto>>> AddCv(AddCvDto newCv)
        {
           var serviceResponse = new ServiceResponse<List<GetCvDto>>();
            Cv cv = _mapper.Map<Cv>(newCv);
                cv.User = await _context.Users.FirstOrDefaultAsync(u => u.ID == GetUserId());

            _context.Cvs.Add(cv);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Cvs
                 .Where(c => c.User.ID == GetUserId())
                .Select(c => _mapper.Map<GetCvDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCvDto>> GetCvsById()
        {
            
            var serviceResponse = new ServiceResponse<GetCvDto>();
            var dbCv = await _context.Cvs

                 .FirstOrDefaultAsync(c => c.UserID == GetUserId());

            serviceResponse.Data = _mapper.Map<GetCvDto>(dbCv);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCvDto>> UpdateCv(UpdateCvDto updatedCv)
        {
             var serviceResponse = new ServiceResponse<GetCvDto>();
            try
            {
                Cv cv = await _context.Cvs
                     .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.ID == updatedCv.ID);
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
    }
}