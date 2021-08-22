using System;
using System.Collections.Generic;

namespace hp_proj_1_backend.Models
{
    public class User
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
        public DateTime CreatedAt { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Job> Jobs { get; set; }
        public List<AppliedJob> AppliedJobs { get; set; }
        public Cv Cv { get; set; }

        public static implicit operator int(User v)
        {
            throw new NotImplementedException();
        }
    }
}