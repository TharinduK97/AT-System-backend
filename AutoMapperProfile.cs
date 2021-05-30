using AutoMapper;
using hp_proj_1_backend.Dtos.JobDtos;
using hp_proj_1_backend.Models;

namespace hp_proj_1_backend
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Job, GetJobDto>();
             CreateMap<AddJobDto, Job>();
        }
    }
}