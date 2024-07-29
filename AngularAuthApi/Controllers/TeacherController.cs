using DataAccess.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Request;
using Models.DTOs.Response;
using System.Net;

namespace AngularAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepo _teacherRepo;

        public TeacherController(ITeacherRepo teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }
        #region TeacherSubjects


        [HttpPost("SaveTeacherSubject")]
        public async Task<IActionResult> SaveTeacherSubject([FromBody] SaveSubjectTeacherRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _teacherRepo.SaveTeacherSubjectAsync(request);
                if (result == 1)
                {
                    return Ok(new
                    {
                        Status = HttpStatusCode.OK,
                        Data = (object)null,
                        Message = "Data saved successfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "Failed to save data"
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Data = (object)null,
                    Message = "Invalid request"
                });
            }
        }

       



        #endregion


    }
}
