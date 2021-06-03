using AutoMapper;
using hp_proj_1_backend.Dtos.Applied_JobDto;
using hp_proj_1_backend.Dtos.JobDtos;
using hp_proj_1_backend.Dtos.UserDto;
using hp_proj_1_backend.Models;

namespace hp_proj_1_backend
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Job, GetJobDto>();
             CreateMap<AddJobDto, Job>();
               CreateMap<AppliedJob, GetAppliedJobDto>();
             CreateMap<AddAppliedJobDto, AppliedJob>();
               CreateMap<User, GetUserDetailsDto>();
        }
    }
}