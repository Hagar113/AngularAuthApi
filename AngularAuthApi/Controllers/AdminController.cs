using DataProvider.IProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                
                var saveRoleRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveRoleReques>(baseRequestHeader.data.ToString());

                if (saveRoleRequest == null)
                {
                    return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidData"]);
                }

                var status = await _adminProvider.AdminRepo.SaveRole(saveRoleRequest);

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
    }
}
