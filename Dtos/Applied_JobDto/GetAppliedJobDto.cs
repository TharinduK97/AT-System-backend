using System;
using hp_proj_1_backend.Dtos.UserDto;
using hp_proj_1_backend.Models;

namespace hp_proj_1_backend.Dtos.Applied_JobDto
{
    public class GetAppliedJobDto
    {
        public int ID { get; set; }
         public string JobStatus { get; set; }
           public DateTime CreatedAt { get; set; }
            public int JobID { get; set; }
         

            public GetUserDetailsDto User { get; set; }
        //      public Job Job { get; set; }

    }
}