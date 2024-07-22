using DataAccess;
using DataAccess.IRepo;
using DataProvider.IProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Request;
using System.Net;

namespace AngularAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepo _adminRepo;

        public AdminController(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;

        }
        #region subject methods
        [HttpGet("GetAllSubjects")]
        public async Task<IActionResult> GetSubjectDropdown([FromQuery] GetSubjectsByYearRequest request)
        {
            try
            {
                var subjects = await _adminRepo.GetSubjectsDropdown(request);
                return Ok(subjects);
            }
            catch
            {
                return BadRequest(new { msg = "حدث خطأ برجاء المحاولة في وقت لاحق" });
            }
        }


        [HttpPost("SaveSubject")]
        public async Task<IActionResult> SaveSubject(SaveSubjectRequest saveSubjectsReq)
        {
            try
            {
                var status = await _adminRepo.SaveSubject(saveSubjectsReq);
                if (status == 1)
                {
                    return Ok(new
                    {
                        Status = HttpStatusCode.OK,
                        Data = (object)null,
                        Message = "Save SuccessFully"
                    });
                }
                else if (status == -1)
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "An error occurred"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "Id not found "
                    });
                }
            }
            catch
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Data = (object)null,
                    Message = "An error occurred"
                });
            }
        }

        [HttpPost("GetSubjectById")]
        public async Task<IActionResult> GetSubjectById(SubjectRequest request)
        {
            try
            {
                var subject = await _adminRepo.GetSubjectById(request);
                if (subject != null)
                {
                    return Ok(new
                    {
                        Status = HttpStatusCode.OK,
                        Data = subject,
                        Message = "تم بنجاح"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "المادة غير موجودة"
                    });
                }
            }
            catch
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Data = (object)null,
                    Message = "حدث خطأ برجاء المحاولة في وقت لاحق"
                });
            }
        }

        [HttpPost("DeleteSubject")]
        public async Task<IActionResult> DeleteSubject(SubjectRequest subjectRequest)
        {
            if (subjectRequest == null || subjectRequest.id <= 0)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Data = (object)null,
                    Message = "Invalid request data"
                });
            }

            try
            {
                var isDeleted = await _adminRepo.DeleteSubject(subjectRequest);
                if (isDeleted)
                {
                    return Ok(new
                    {
                        Status = HttpStatusCode.OK,
                        Data = (object)null,
                        Message = "Successfully deleted"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "Subject not found or already deleted"
                    });
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while deleting subject: {ex.Message}");
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Data = (object)null,
                    Message = "An error occurred"
                });
            }
        }
        #endregion

        #region student methods
        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var student = await _adminRepo.GetAllStudents();
                return Ok(student);
            }
            catch { return BadRequest(new { msg = "حدث خطأ برجاء المحاولة في وقت لاحق" }); }
        }

        [HttpPost("SaveStudent")]
        public async Task<IActionResult> SaveStudent(SaveStudentRequest saveStudentsReq)
        {
            try
            {
                var status = await _adminRepo.SaveStudent(saveStudentsReq);
                if (status == 1)
                {
                    return Ok(new
                    {
                        Status = HttpStatusCode.OK,
                        Data = (object)null,
                        Message = "Save SuccessFully"
                    });
                }
                else if (status == -1)
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "An error occurred"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "Id not found "
                    });
                }
            }
            catch
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Data = (object)null,
                    Message = "An error occurred"
                });
            }
        }

        [HttpPost("GetStudentById")]
        public async Task<IActionResult> GetStudentById(studentReq request)
        {
            try
            {
                var student = await _adminRepo.GetStudentById(request);
                if (student != null)
                {
                    return Ok(new
                    {
                        Status = HttpStatusCode.OK,
                        Data = student,
                        Message = "تم بنجاح"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "المادة غير موجودة"
                    });
                }
            }
            catch
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Data = (object)null,
                    Message = "حدث خطأ برجاء المحاولة في وقت لاحق"
                });
            }
        }



        #endregion

        #region teacher
        [HttpGet("GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var teacher = await _adminRepo.GetAllTeachers();
                return Ok(teacher);
            }
            catch { return BadRequest(new { msg = "حدث خطأ برجاء المحاولة في وقت لاحق" }); }
        }

        [HttpPost("SaveTeachers")]
        public async Task<IActionResult> SaveTeacher(SaveTeacherRequest saveTeachersReq)
        {
            try
            {
                var status = await _adminRepo.SaveTeacher(saveTeachersReq);
                if (status == 1)
                {
                    return Ok(new
                    {
                        Status = HttpStatusCode.OK,
                        Data = (object)null,
                        Message = "Save SuccessFully"
                    });
                }
                else if (status == -1)
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "An error occurred"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "Id not found "
                    });
                }
            }
            catch
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Data = (object)null,
                    Message = "An error occurred"
                });
            }
        }

        [HttpPost("GetTeacherById")]
        public async Task<IActionResult> GetTeacherById(TeacherRequest request)
        {
            try
            {
                var teacher = await _adminRepo.GetTeacherById(request);
                if (teacher != null)
                {
                    return Ok(new
                    {
                        Status = HttpStatusCode.OK,
                        Data = teacher,
                        Message = "تم بنجاح"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Data = (object)null,
                        Message = "teacher not found"
                    });
                }
            }
            catch
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Data = (object)null,
                    Message = "an error occour"
                });
            }
        }


       
        #endregion

    }
}
