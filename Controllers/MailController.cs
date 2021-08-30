using System;
using System.Threading.Tasks;
using hp_proj_1_backend_master.Models;
using hp_proj_1_backend_master.Services.MailService;
using Microsoft.AspNetCore.Mvc;

namespace hp_proj_1_backend_master.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController:  ControllerBase
    {
        
        private readonly IMailService mailService;

    public MailController(IMailService mailService)
    {
        this.mailService = mailService;
    }

       [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromForm]MailRequest request)
    {
        try
        {
            await mailService.SendEmailAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {

            throw ex;
        }
            
    }


    }
}