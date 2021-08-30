using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace hp_proj_1_backend_master.Models
{
    public class MailRequest
    {
         public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
         public List<IFormFile> Attachments { get; set; }

    }
}