using DataAccess.IRepo;
using DataAccess.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Request;
using DataProvider.Provider;
using Models.DTOs.Response;
using System.Net;

namespace AngularAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepo _studentRepo;

        public StudentController(IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }

       

        [HttpPost("SaveStudentSubjectTeacher")]
        public async Task<IActionResult> SaveStudentSubjectTeacher([FromBody] SaveStudentSubjectTeacher request)
        {
            if (ModelState.IsValid)
            {
                var result = await _studentRepo.SaveStudentSubjectTeacherAsync(request);
                if (result == 1)
                {
                    return Ok(new
                    {
                        Status = HttpStatusCode.OK,
                        Data = (object)null,
                        Message = "Data saved successfully"
                    });
                }
                else if (result == -2)
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "Subject already assigned to a teacher for this student"
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

        
    }
}

