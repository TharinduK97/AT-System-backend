using System.Collections.Generic;
using hp_proj_1_backend.Dtos.Applied_JobDto;
using hp_proj_1_backend.Dtos.JobDtos;
using hp_proj_1_backend.Models;

namespace hp_proj_1_backend.Dtos.UserDto
{
    public class GetUserDetailsDto
    {
         public int ID { get; set; }
         public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNum { get; set; }
        public int WorkExperience { get; set; }
        public string Skills { get; set; }
        public string ImgUrl { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        // public List<GetAppliedJobDto> AppliedJobs { get; set; }
       
    }
}