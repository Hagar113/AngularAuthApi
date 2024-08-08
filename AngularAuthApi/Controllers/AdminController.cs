using DataProvider.IProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Models.DTOs.BaseRequest;
using Models.DTOs.Request;
using Models.DTOs.Response;
using Models.models;
using Newtonsoft.Json;
using System.Net;

namespace AngularAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminProvider _adminProvider;
        private readonly IStringLocalizer _localizer;
        public AdminController(IAdminProvider adminProvider, IStringLocalizer<AuthController> localizer)
        {
            _adminProvider = adminProvider;
            _localizer = localizer;

        }

        #region roles
        [HttpPost("GetRoleById")]
        public async Task<IActionResult> GetRoleById([FromBody] BaseRequestHeader request)
        {
            GeneralResponse response;

            try
            {
                if (request == null || request.data == null)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                RoleRequest roleRequest;
                try
                {
                    roleRequest = JsonConvert.DeserializeObject<RoleRequest>(request.data.ToString());
                }
                catch (JsonException)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                if (roleRequest == null || roleRequest.RoleId <= 0)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                // Removed redundant declaration of roleRequest
                var role = await _adminProvider.AdminRepo.GetRoleById(roleRequest);
                if (role != null)
                {
                    response = GeneralResponse.Create(HttpStatusCode.OK, role, _localizer["DataRetrievedSuccessfully"]);
                    return Ok(response);
                }
                else
                {
                    response = GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["RoleNotFound"]);
                    return NotFound(response);
                }
            }
            catch
            {
                response = GeneralResponse.Create(HttpStatusCode.InternalServerError, null, _localizer["ErrorOccurred"], new { msg = _localizer["ErrorOccurred"] });
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _adminProvider.AdminRepo.GetAllRoles();
                var response = GeneralResponse.Create(HttpStatusCode.OK, roles, _localizer["Rolesretrievedsuccessfully"]);
                return Ok(response);
            }
            catch

            {
                var errorResponse = GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"], new { msg = _localizer["ErrorOccurred"] });
                return BadRequest(errorResponse);
            }
        }


        [HttpPost("DeleteRole")]
        public async Task<GeneralResponse> DeleteRole([FromBody] BaseRequestHeader baseRequestHeader)
        {
            if (baseRequestHeader == null || string.IsNullOrEmpty(baseRequestHeader.data.ToString()))
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["DataNotFound"]);
            }

            try
            {
                // Deserialize the 'data' field to get the roleId
                var roleRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<RoleRequest>(baseRequestHeader.data.ToString());

                var result = await _adminProvider.AdminRepo.DeleteRole(roleRequest);

                if (result)
                {
                    return GeneralResponse.Create(HttpStatusCode.OK, null, _localizer["RoleDeletedSuccessfully"]);
                }
                else
                {
                    return GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["FailedToDeleteRole"]);
                }
            }
            catch
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
            }
        }


        [HttpPost("SaveRole")]
        public async Task<GeneralResponse> SaveRole([FromBody] BaseRequestHeader baseRequestHeader)
        {
            try
            {
                // Ensure baseRequestHeader is not null
                if (baseRequestHeader == null)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"]);
                }

                // Deserialize the 'data' object to SaveRoleReques
                SaveRoleReques saveRoleRequest = null;

                if (baseRequestHeader.data != null)
                {
                    var jsonData = baseRequestHeader.data.ToString();
                    saveRoleRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveRoleReques>(jsonData);
                }

                // Check if deserialization was successful
                if (saveRoleRequest == null)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"]);
                }

                // Extract the list of selected page IDs from the request
                var selectedPageIds = saveRoleRequest.SelectedPageIds;

                if (selectedPageIds == null)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"]);
                }

                // Call the SaveRole method in the repository
                var status = await _adminProvider.AdminRepo.SaveRole(saveRoleRequest, selectedPageIds);

                // Determine the response based on the status
                if (status == 1)
                {
                    return GeneralResponse.Create(HttpStatusCode.OK, null, _localizer["RoleSavedSuccessfully"]);
                }
                else if (status == -1)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
                }
                else if (status == -2)
                {
                    return GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["RoleNotFound"]);
                }
                else
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["UnexpectedError"]);
                }
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
            }
        }



        #endregion


        #region

        [HttpPost("GetSubjectById")]
        public async Task<IActionResult> GetSubjectById([FromBody] BaseRequestHeader request)
        {
            GeneralResponse response;

            try
            {
                if (request == null || request.data == null)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                SubjectRequest subjectRequest;
                try
                {
                    subjectRequest = JsonConvert.DeserializeObject<SubjectRequest>(request.data.ToString());
                }
                catch (JsonException)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                if (subjectRequest == null || subjectRequest.SubjectId <= 0)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                var subject = await _adminProvider.AdminRepo.GetSubjectById(subjectRequest);
                if (subject != null)
                {
                    response = GeneralResponse.Create(HttpStatusCode.OK, subject, _localizer["DataRetrievedSuccessfully"]);
                    return Ok(response);
                }
                else
                {
                    response = GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["SubjectNotFound"]);
                    return NotFound(response);
                }
            }
            catch
            {
                response = GeneralResponse.Create(HttpStatusCode.InternalServerError, null, _localizer["ErrorOccurred"], new { msg = _localizer["ErrorOccurred"] });
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("GetAllSubjects")]
        public async Task<IActionResult> GetAllSubjects([FromBody] BaseRequestHeader baseRequestHeader)
        {
            try
            {
                var subjects = await _adminProvider.AdminRepo.GetAllSubjects();
                var response = GeneralResponse.Create(HttpStatusCode.OK, subjects, _localizer["SubjectsRetrievedSuccessfully"]);
                return Ok(response);
            }
            catch
            {
                var errorResponse = GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"], new { msg = _localizer["ErrorOccurred"] });
                return BadRequest(errorResponse);
            }
        }

        [HttpPost("DeleteSubject")]
        public async Task<GeneralResponse> DeleteSubject([FromBody] BaseRequestHeader baseRequestHeader)
        {
            if (baseRequestHeader == null || string.IsNullOrEmpty(baseRequestHeader.data.ToString()))
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["DataNotFound"]);
            }

            try
            {
                // Deserialize the 'data' field to get the SubjectId
                var subjectRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<SubjectRequest>(baseRequestHeader.data.ToString());

                var result = await _adminProvider.AdminRepo.DeleteSubject(subjectRequest);

                if (result)
                {
                    return GeneralResponse.Create(HttpStatusCode.OK, null, _localizer["SubjectDeletedSuccessfully"]);
                }
                else
                {
                    return GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["FailedToDeleteSubject"]);
                }
            }
            catch
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
            }
        }

        [HttpPost("SaveSubject")]
        public async Task<GeneralResponse> SaveSubject([FromBody] BaseRequestHeader baseRequestHeader)
        {
            try
            {
                var saveSubjectRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveSubjectRequest>(baseRequestHeader.data.ToString());

                if (saveSubjectRequest == null)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"]);
                }

                var status = await _adminProvider.AdminRepo.SaveSubject(saveSubjectRequest);

                if (status == 1)
                {
                    return GeneralResponse.Create(HttpStatusCode.OK, null, _localizer["SubjectSavedSuccessfully"]);
                }
                else if (status == -1)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
                }
                else if (status == -2)
                {
                    return GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["SubjectNotFound"]);
                }
                else
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["UnexpectedError"]);
                }
            }
            catch
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
            }
        }

        #endregion


        #region pages

        [HttpPost("GetPageById")]
        public async Task<IActionResult> GetPageById([FromBody] BaseRequestHeader request)
        {
            GeneralResponse response;

            try
            {
                if (request == null || request.data == null)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                PageRequest pageRequest;
                try
                {
                    pageRequest = JsonConvert.DeserializeObject<PageRequest>(request.data.ToString());
                }
                catch (JsonException)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                if (pageRequest == null || pageRequest.PageId <= 0)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                var page = await _adminProvider.AdminRepo.GetPageById(pageRequest);
                if (page != null)
                {
                    response = GeneralResponse.Create(HttpStatusCode.OK, page, _localizer["DataRetrievedSuccessfully"]);
                    return Ok(response);
                }
                else
                {
                    response = GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["PageNotFound"]);
                    return NotFound(response);
                }
            }
            catch
            {
                response = GeneralResponse.Create(HttpStatusCode.InternalServerError, null, _localizer["ErrorOccurred"], new { msg = _localizer["ErrorOccurred"] });
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllPages")]
        public async Task<IActionResult> GetAllPages()
        {
            try
            {
                var pages = await _adminProvider.AdminRepo.GetAllPages();
                var response = GeneralResponse.Create(HttpStatusCode.OK, pages, _localizer["PagesRetrievedSuccessfully"]);
                return Ok(response);
            }
            catch
            {
                var errorResponse = GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"], new { msg = _localizer["ErrorOccurred"] });
                return BadRequest(errorResponse);
            }
        }

        [HttpPost("DeletePage")]
        public async Task<GeneralResponse> DeletePage([FromBody] BaseRequestHeader baseRequestHeader)
        {
            if (baseRequestHeader == null || string.IsNullOrEmpty(baseRequestHeader.data.ToString()))
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["DataNotFound"]);
            }

            try
            {
                var pageRequest = JsonConvert.DeserializeObject<PageRequest>(baseRequestHeader.data.ToString());

                var result = await _adminProvider.AdminRepo.DeletePage(pageRequest);

                if (result)
                {
                    return GeneralResponse.Create(HttpStatusCode.OK, null, _localizer["PageDeletedSuccessfully"]);
                }
                else
                {
                    return GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["FailedToDeletePage"]);
                }
            }
            catch
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
            }
        }

        //[HttpPost("SaveRole")]
        //public async Task<GeneralResponse> SaveRole([FromBody] SaveRoleReques saveRoleRequest, [FromQuery] List<int> pageIds)
        //{
        //    try
        //    {
        //        var status = await _adminProvider.AdminRepo.SaveRole(saveRoleRequest, pageIds);

        //        if (status == 1)
        //        {
        //            return GeneralResponse.Create(HttpStatusCode.OK, null, _localizer["RoleSavedSuccessfully"]);
        //        }
        //        else if (status == -1)
        //        {
        //            return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
        //        }
        //        else if (status == -2)
        //        {
        //            return GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["RoleNotFound"]);
        //        }
        //        else
        //        {
        //            return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["UnexpectedError"]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception if needed
        //        return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
        //    }
        //}



        #endregion


        #region users
        [HttpPost("GetUserById")]
        public async Task<IActionResult> GetUserById([FromBody] BaseRequestHeader request)
        {
            GeneralResponse response;

            try
            {
                if (request == null || request.data == null)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                UserReqById userRequest;
                try
                {
                    userRequest = JsonConvert.DeserializeObject<UserReqById>(request.data.ToString());
                }
                catch (JsonException)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                if (userRequest == null || userRequest.userId <= 0)
                {
                    return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"], new { msg = _localizer["InvalidData"] }));
                }

                var user = await _adminProvider.AdminRepo.GetUserById(userRequest);
                if (user != null)
                {
                    response = GeneralResponse.Create(HttpStatusCode.OK, user, _localizer["DataRetrievedSuccessfully"]);
                    return Ok(response);
                }
                else
                {
                    response = GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["UserNotFound"]);
                    return NotFound(response);
                }
            }
            catch
            {
                response = GeneralResponse.Create(HttpStatusCode.InternalServerError, null, _localizer["ErrorOccurred"], new { msg = _localizer["ErrorOccurred"] });
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }



        [HttpPost("SaveUser")]
        public async Task<GeneralResponse> SaveUser([FromBody] BaseRequestHeader baseRequestHeader)
        {
            try
            {
                var saveUserRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveUserRequest>(baseRequestHeader.data.ToString());

                if (saveUserRequest == null)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"]);
                }

                var status = await _adminProvider.AdminRepo.SaveUser(saveUserRequest);

                if (status == 1)
                {
                    return GeneralResponse.Create(HttpStatusCode.OK, null, _localizer["UserSavedSuccessfully"]);
                }
                else if (status == -1)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
                }
                else if (status == -2)
                {
                    return GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["UserNotFound"]);
                }
                else
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["UnexpectedError"]);
                }
            }
            catch
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
            }
        }
        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromBody] BaseRequestHeader baseRequestHeader)
        {
            try
            {
                var users = await _adminProvider.AdminRepo.GetAllUsers();
                var response = GeneralResponse.Create(HttpStatusCode.OK, users, _localizer["UsersRetrievedSuccessfully"]);
                return Ok(response);
            }
            catch
            {
                var errorResponse = GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"], new { msg = _localizer["ErrorOccurred"] });
                return BadRequest(errorResponse);
            }
        }
        [HttpPost("DeleteUser")]
        public async Task<GeneralResponse> DeleteUser([FromBody] BaseRequestHeader baseRequestHeader)
        {
            if (baseRequestHeader == null || string.IsNullOrEmpty(baseRequestHeader.data.ToString()))
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["DataNotFound"]);
            }

            try
            {
                var userRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<UserReqById>(baseRequestHeader.data.ToString());

                var result = await _adminProvider.AdminRepo.DeleteUser(userRequest);

                if (result)
                {
                    return GeneralResponse.Create(HttpStatusCode.OK, null, _localizer["UserDeletedSuccessfully"]);
                }
                else
                {
                    return GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["FailedToDeleteUser"]);
                }
            }
            catch
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"]);
            }
        }
        //[HttpPost("SaveTeacherSubject")]
        //public async Task<IActionResult> SaveTeacherSubject([FromBody] SaveSubjectTeacherRequest request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _adminProvider.AdminRepo.SaveTeacherSubjectAsync(request);
        //        if (result == 1)
        //        {
        //            return Ok(new
        //            {
        //                Status = HttpStatusCode.OK,
        //                Data = (object)null,
        //                Message = "Data saved successfully"
        //            });
        //        }
        //        else
        //        {
        //            return BadRequest(new
        //            {
        //                Status = HttpStatusCode.BadRequest,
        //                Data = (object)null,
        //                Message = "Failed to save data"
        //            });
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest(new
        //        {
        //            Status = HttpStatusCode.BadRequest,
        //            Data = (object)null,
        //            Message = "Invalid request"
        //        });
        //    }
        //}

        [HttpPost("GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers([FromBody] BaseRequestHeader baseRequestHeader)
        {
            try
            {
                // Fetch all teacher names from the provider
                var teachers = await _adminProvider.AdminRepo.GetAllTeacherNames();

                // Create a success response
                var response = GeneralResponse.Create(HttpStatusCode.OK, teachers, _localizer["TeachersRetrievedSuccessfully"]);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                var errorResponse = GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ErrorOccurred"], new { msg = _localizer["ErrorOccurred"], exception = ex.Message });
                return BadRequest(errorResponse);
            }



        }
        [HttpPost("SaveTeacherSubject")]
        public async Task<GeneralResponse> SaveTeacherSubject([FromBody] BaseRequestHeader baseRequestHeader)
        {
            try
            {
                
                var assignSubjectRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AssignSubjectToTeacherRequest>(baseRequestHeader.data.ToString());

                if (assignSubjectRequest == null)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"]);
                }

                
                var result = await _adminProvider.AdminRepo.AssignSubjectToTeacher(assignSubjectRequest);

                switch (result)
                {
                    case 1:
                        return GeneralResponse.Create(HttpStatusCode.OK, null, _localizer["DataSavedSuccessfully"]);
                    case -1:
                        return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["FailedToSaveData"]);
                    case -2:
                        return GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["TeacherOrSubjectNotFound"]);
                    default:
                        return GeneralResponse.Create(HttpStatusCode.InternalServerError, null, _localizer["UnexpectedError"]);
                }
            }
            catch (Exception ex)
            {
                return GeneralResponse.Create(HttpStatusCode.InternalServerError, null, _localizer["ErrorOccurred"], new { ErrorDetails = ex.Message });
            }
        }


        //[HttpPost("GetTeacherWithAssignedSubject")]
        //public async Task<IActionResult> GetTeacherWithAssignedSubject([FromBody] BaseRequestHeader baseRequestHeader)
        //{
        //    try
        //    {
        //        if (!int.TryParse(baseRequestHeader.data.ToString(), out var teacherId))
        //        {
        //            return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidTeacherId"]));
        //        }

        //        var subject = await _adminProvider.AdminRepo.GetAssignedSubjectForTeacher(teacherId);

        //        if (subject == null)
        //        {
        //            return NotFound(GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["SubjectNotFound"]));
        //        }

        //        return Ok(GeneralResponse.Create(HttpStatusCode.OK, subject, _localizer["TeacherAndSubjectRetrievedSuccessfully"]));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, GeneralResponse.Create(HttpStatusCode.InternalServerError, null, _localizer["ErrorOccurred"], new { ErrorDetails = ex.Message }));
        //    }
        //}
        [HttpPost("GetAssignedSubject")]
        public async Task<GeneralResponse> GetAssignedSubject([FromBody] BaseRequestHeader baseRequestHeader)
        {
            try
            {
                // تحقق من أن البيانات موجودة وأنها من نوع `string`
                if (baseRequestHeader == null || baseRequestHeader.data == null)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, "Invalid request data");
                }

                // تحويل `baseRequestHeader.Data` إلى `string` إذا كان من نوع `object`
                var jsonData = baseRequestHeader.data.ToString();
                if (jsonData == null)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, "Data conversion error");
                }

                // ديسرياليز البيانات إلى `TeacherSubjectRequest`
                var request = Newtonsoft.Json.JsonConvert.DeserializeObject<TeacherSubjectRequest>(jsonData);

                if (request == null || request.teacherId <= 0)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, "Invalid data");
                }

                var subject = await _adminProvider.AdminRepo.GetAssignedSubjectForTeacherAsync(request.teacherId);

                if (subject != null)
                {
                    return GeneralResponse.Create(HttpStatusCode.OK, new
                    {
                        subject.Id,
                        subject.Name,
                        subject.AcademicYear
                    }, "Subject retrieved successfully");
                }
                else
                {
                    return GeneralResponse.Create(HttpStatusCode.NotFound, null, "No subject assigned to the specified teacher");
                }
            }
            catch (Exception ex)
            {
                return GeneralResponse.Create(HttpStatusCode.InternalServerError, null, "An error occurred", ex.Message);
            }
        }


        #endregion


    }
}
