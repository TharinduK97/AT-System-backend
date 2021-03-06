using System;

namespace hp_proj_1_backend.Dtos.UserDto
{
    public class UserRegisterDto
    {
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
        public string Password { get; set; }
    }
}