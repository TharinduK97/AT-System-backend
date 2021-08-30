using System;

namespace hp_proj_1_backend.Dtos.JobDtos
{
    public class UpdateJobDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Skills { get; set; }
        public string Salary { get; set; }
        public string Description { get; set; }
        public string JobStatus { get; set; }
        public DateTime LimitLine { get; set; }
        public string FullPart { get; set; }
    }
}