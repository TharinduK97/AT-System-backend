using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using hp_proj_1_backend.Models;
using hp_proj_1_backend_master.Dtos.CvDto;
using hp_proj_1_backend_master.Services.CvService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hp_proj_1_backend_master.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CvController: ControllerBase
    {
        private readonly ICvService _cvservice;

        public CvController(ICvService cvservice)
        {
            _cvservice = cvservice;

        }

        [HttpGet("GetAll")]
         public async Task<ActionResult<ServiceResponse<GetCvDto>>> GetSingle()
        {
            return Ok(await _cvservice.GetCvsById());
        }

        //  [Authorize(Roles = "Applicant")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCvDto>>>> AddCv(AddCvDto newcv)
        {
            return Ok(await  _cvservice.AddCv(newcv));
        }

          [Authorize(Roles = "Applicant")]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCvDto>>> UpdateCv(UpdateCvDto updatedCv)
        {
            var response = await _cvservice.UpdateCv(updatedCv);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpPost("Store"), DisableRequestSizeLimit]
public async Task<IActionResult> UploadAsync()
{
    try
    {
        var formCollection = await Request.ReadFormAsync();
        var file = formCollection.Files.First();
        var folderName = Path.Combine("Resources", "Cvs");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        if (file.Length > 0)
        {
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName
                                                        .Trim('"');
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok(new { dbPath });
        }
        else
        {
            return BadRequest();
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex}");
    }
}

    }
}