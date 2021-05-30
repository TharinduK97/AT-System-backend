using System.Collections.Generic;
using System.Threading.Tasks;
using hp_proj_1_backend.Dtos.JobDtos;
using hp_proj_1_backend.Models;

namespace hp_proj_1_backend.Services.JobService
{
    public interface IJobService
    {
         Task<ServiceResponse<List<GetJobDto>>> GetAllJobs();
        Task<ServiceResponse<GetJobDto>> GetJobsById(int id);
        Task<ServiceResponse<List<GetJobDto>>> AddJob(AddJobDto newJob);
        Task<ServiceResponse<GetJobDto>> UpdateJob(UpdateJobDto updatedJob);
        Task<ServiceResponse<List<GetJobDto>>> DeleteJob(int id);
    }
}