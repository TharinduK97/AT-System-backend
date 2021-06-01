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

        public async Task<ServiceResponse<List<GetAppliedJobDto>>> AddAppliedJob(AddAppliedJobDto newApplliedJob)
        {
            var serviceResponse = new ServiceResponse<List<GetAppliedJobDto>>();
            AppliedJob appliedJob = _mapper.Map<AppliedJob>(newApplliedJob);
            appliedJob.User = await _context.Users.FirstOrDefaultAsync(u => u.ID == GetUserId());

            _context.AppliedJobs.Add(appliedJob);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.AppliedJobs
                .Where(c => c.User.ID == GetUserId())
                .Select(c => _mapper.Map<GetAppliedJobDto>(c)).ToListAsync();
            return serviceResponse;
             throw new System.NotImplementedException();
           
        }

        public Task<ServiceResponse<List<GetAppliedJobDto>>> DeleteAppliedJob(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<List<GetAppliedJobDto>>> GetAllAppliedJobs()
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<GetAppliedJobDto>> GetAppliedJobsById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}