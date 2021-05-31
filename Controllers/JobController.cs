using System.Threading.Tasks;
using hp_proj_1_backend.Services.JobService;
using Microsoft.AspNetCore.Mvc;
using hp_proj_1_backend.Models;
using System.Collections.Generic;
using hp_proj_1_backend.Dtos.JobDtos;
using Microsoft.AspNetCore.Authorization;

namespace hp_proj_1_backend.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobservice;

        public JobController(IJobService jobservice)
        {
            _jobservice = jobservice;

        }
         [Authorize]
         [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetJobDto>>>> Get()
        {
            return Ok(await _jobservice.GetAllJobs());
        }

         [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetJobDto>>> GetSingle(int id)
        {
            return Ok(await _jobservice.GetJobsById(id));
        }
         [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetJobDto>>>> AddCharacter(AddJobDto newCharacter)
        {
            return Ok(await  _jobservice.AddJob(newCharacter));
        }
         [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetJobDto>>> UpdateCharacter(UpdateJobDto updatedCharacter)
        {
            var response = await _jobservice.UpdateJob(updatedCharacter);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
         [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetJobDto>>>> Delete(int id)
        {
            var response = await  _jobservice.DeleteJob(id);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}