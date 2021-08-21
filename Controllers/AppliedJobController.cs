using System.Collections.Generic;
using System.Threading.Tasks;
using hp_proj_1_backend.Dtos.Applied_JobDto;
using hp_proj_1_backend.Models;
using hp_proj_1_backend.Services.AppliedJobService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hp_proj_1_backend.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class AppliedJobController : ControllerBase
    {

        private readonly IAppliedJobService _appliedJobService;

        public AppliedJobController(IAppliedJobService appliedJobService)
        {
            _appliedJobService = appliedJobService;

        }
        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetAppliedJobDto>>>> Get()
        {
            return Ok(await  _appliedJobService.GetAllAppliedJobs());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAppliedJobDto>>> GetSingle(int id)
        {
            return Ok(await _appliedJobService.GetAppliedJobsById(id));
        }

         [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetAppliedJobDto>>>> AddAppliedJob(AddAppliedJobDto newAppliedJob)
        {
            return Ok(await _appliedJobService.AddAppliedJob(newAppliedJob));
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetAppliedJobDto>>> UpdateCharacter(UpdateAppliedJobDto updatedCharacter)
        {
            var response = await _appliedJobService.UpdateAppliedJob(updatedCharacter);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetAppliedJobDto>>>> Delete(int id)
        {
            var response = await _appliedJobService.DeleteAppliedJob(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}