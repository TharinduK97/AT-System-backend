using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using hp_proj_1_backend.Data;
using hp_proj_1_backend.Dtos.Applied_JobDto;
using hp_proj_1_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hp_proj_1_backend.Services.AppliedJobService
{
    public class AppliedJobService : IAppliedJobService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppliedJobService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;

        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        public async Task<ServiceResponse<List<GetAppliedJobDto>>> AddAppliedJob(AddAppliedJobDto newApplliedJob)
        {
            var serviceResponse = new ServiceResponse<List<GetAppliedJobDto>>();
            AppliedJob appliedJob = _mapper.Map<AppliedJob>(newApplliedJob);
             appliedJob.User = await _context.Users.FirstOrDefaultAsync(u => u.ID == GetUserId());
             appliedJob.Job = await _context.Jobs.FirstOrDefaultAsync(u => u.ID == newApplliedJob.JobID);
            _context.AppliedJobs.Add(appliedJob);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.AppliedJobs
                .Where(c => c.User.ID == GetUserId())
                .Select(c => _mapper.Map<GetAppliedJobDto>(c)).ToListAsync();
            return serviceResponse;
             
        }

        public async Task<ServiceResponse<List<GetAppliedJobDto>>> DeleteAppliedJob(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetAppliedJobDto>>();
            try
            {
                AppliedJob appliedJob = await _context.AppliedJobs
                    .FirstOrDefaultAsync(c => c.ID == id );
                if (appliedJob != null)
                {
                    _context.AppliedJobs.Remove(appliedJob);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.AppliedJobs
                
                        .Select(c => _mapper.Map<GetAppliedJobDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Applied Job is not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAppliedJobDto>>> GetAllAppliedJobs()
        {
            var serviceResponse = new ServiceResponse<List<GetAppliedJobDto>>();
            var dbCharacters =  GetUserRole().Equals("Admin") ?
            await _context.AppliedJobs
                
                .ToListAsync()
            :await _context.AppliedJobs
              
                .Where(c => c.User.ID == GetUserId()).ToListAsync();
                  
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetAppliedJobDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAppliedJobDto>> GetAppliedJobsById(int id) 
        {
        var serviceResponse = new ServiceResponse<GetAppliedJobDto>();
            var dbAppliedJob =  GetUserRole().Equals("Admin") ?
            await _context.AppliedJobs 
                 .Include(c => c.Job)
                .FirstOrDefaultAsync(c => c.ID == id)
             :
             await _context.AppliedJobs
                .Include(c => c.Job)
                .FirstOrDefaultAsync(c => c.ID == id && c.User.ID == GetUserId());
            serviceResponse.Data = _mapper.Map<GetAppliedJobDto>(dbAppliedJob);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAppliedJobDto>> UpdateAppliedJob(UpdateAppliedJobDto updatedAppliedJob)
        {
             var serviceResponse = new ServiceResponse<GetAppliedJobDto>();
            try
            {
                AppliedJob appliedJob = await _context.AppliedJobs
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.ID == updatedAppliedJob.ID);
                if (GetUserRole().Equals("Admin"))
                {
                   appliedJob.JobStatus = updatedAppliedJob.JobStatus;
                   

                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetAppliedJobDto>(appliedJob);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Applied Job is not found.";
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