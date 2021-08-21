using System.Collections.Generic;
using System.Threading.Tasks;
using hp_proj_1_backend.Models;
using hp_proj_1_backend_master.Dtos.CvDto;

namespace hp_proj_1_backend_master.Services.CvService
{
    public interface ICvService
    {
         Task<ServiceResponse<GetCvDto>> GetCvsById();
        Task<ServiceResponse<List<GetCvDto>>> AddCv(AddCvDto newCv);
        Task<ServiceResponse<GetCvDto>> UpdateCv(UpdateCvDto updatedCv);
    }
}