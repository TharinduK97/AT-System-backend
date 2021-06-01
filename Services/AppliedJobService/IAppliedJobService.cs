using System.Collections.Generic;
using System.Threading.Tasks;
using hp_proj_1_backend.Dtos.Applied_JobDto;
using hp_proj_1_backend.Models;

namespace hp_proj_1_backend.Services.AppliedJobService
{
    public interface IAppliedJobService
    {
          Task<ServiceResponse<List<GetAppliedJobDto>>> GetAllAppliedJobs();
        Task<ServiceResponse<GetAppliedJobDto>> GetAppliedJobsById(int id);
        Task<ServiceResponse<List<GetAppliedJobDto>>> AddAppliedJob(AddAppliedJobDto newApplliedJob);
      
        Task<ServiceResponse<List<GetAppliedJobDto>>> DeleteAppliedJob(int id);

        Task<ServiceResponse<GetAppliedJobDto>> UpdateAppliedJob(UpdateAppliedJobDto updatedAppliedJob);
    }
}