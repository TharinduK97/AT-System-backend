using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using hp_proj_1_backend.Data;
using hp_proj_1_backend.Dtos.JobDtos;
using hp_proj_1_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hp_proj_1_backend.Services.JobService
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public JobService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;

        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);


            public async Task<ServiceResponse<List<GetJobDto>>> AddJob(AddJobDto newJob)
        {
            var serviceResponse = new ServiceResponse<List<GetJobDto>>();
            Job job = _mapper.Map<Job>(newJob);
                job.User = await _context.Users.FirstOrDefaultAsync(u => u.ID == GetUserId());

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Jobs
                // .Where(c => c.User.Id == GetUserId())
                .Select(c => _mapper.Map<GetJobDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetJobDto>>> DeleteJob(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetJobDto>>();
            try
            {
                Job job = await _context.Jobs
                     .FirstOrDefaultAsync(c => c.ID == id);
                if (job != null)
                {
                    _context.Jobs.Remove(job);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Jobs
                        // .Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetJobDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Job not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetJobDto>>> GetAllJobs()
        {
            var serviceResponse = new ServiceResponse<List<GetJobDto>>();
            var dbJobs = await _context.Jobs.ToListAsync();

            // .Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbJobs.Select(c => _mapper.Map<GetJobDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetJobDto>> GetJobsById(int id)
        {
            var serviceResponse = new ServiceResponse<GetJobDto>();
            var dbJob = await _context.Jobs

                 .FirstOrDefaultAsync(c => c.ID == id);

            serviceResponse.Data = _mapper.Map<GetJobDto>(dbJob);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetJobDto>> UpdateJob(UpdateJobDto updatedJob)
        {
            var serviceResponse = new ServiceResponse<GetJobDto>();
            try
            {
                Job job = await _context.Jobs
                     .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.ID == updatedJob.ID);
                if (job.User.Role == GetUserRole())
                {
                    job.Title = updatedJob.Title;
                    job.Skills = updatedJob.Skills;
                    job.Salary = updatedJob.Salary;
                    job.Description = updatedJob.Description;
                    job.JobStatus = updatedJob.JobStatus;
                    job.LimitLine = updatedJob.LimitLine;
                     job.FullPart = updatedJob.FullPart;

                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetJobDto>(job);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Job not found.";
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