using System;

namespace hp_proj_1_backend.Models
{
    public class AppliedJob
    {
         public int ID { get; set; }
          public string JobStatus { get; set; }
           public DateTime CreatedAt { get; set; }
           public int JobID { get; set; }
          public User User { get; set; }
            public Job Job { get; set; }
    }
}