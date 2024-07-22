using DataAccess;
using DataProvider.IProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Request;
using Models.DTOs.Response;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AngularAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IAuthProvider _authProvider;

        public AuthController(ApplicationDBContext context, IAuthProvider authProvider)
        {
            _context = context;
            _authProvider = authProvider;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<GeneralResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, "Data not found"));
            }

            var result = await _authProvider.AuthenticationRepo.Login(loginRequest);
            if (result == null)
            {
                return NotFound(GeneralResponse.Create(HttpStatusCode.NotFound, null, "User not found"));
            }

            return Ok(GeneralResponse.Create(HttpStatusCode.OK, result, "Login successful"));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<GeneralResponse>> Register(RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, "Data not found"));
            }

            var validationResults = ValidateRegisterRequest(registerRequest);
            if (validationResults.Any())
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, "Validation failed", validationResults));
            }

            if (await _authProvider.AuthenticationRepo.checkIfEmailOrPhoneOrUsernameExists(registerRequest.email, registerRequest.phone))
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, "Email or phone number has been used before"));
            }

            var validation = _authProvider.AuthenticationRepo.CheckPasswordStrength(registerRequest.password);
            if (!string.IsNullOrEmpty(validation))
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, validation));
            }

            var registered = await _authProvider.AuthenticationRepo.UserRegister(registerRequest);
            if (registered)
            {
                return Ok(GeneralResponse.Create(HttpStatusCode.OK, null, "Successfully registered"));
            }
            else
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, "The provided role does not exist"));
            }
        }

        private ICollection<string> ValidateRegisterRequest(RegisterRequest request)
        {
            var errors = new List<string>();
            var context = new ValidationContext(request, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
           
            Validator.TryValidateObject(request, context, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                errors.AddRange(validationResult.MemberNames.Select(memberName => validationResult.ErrorMessage));
            }
            
            if (!new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$").IsMatch(request.email))
            {
                errors.Add("Invalid email address" );
            }

            if (!new Regex(@"^(010|011|012)\d{8}$").IsMatch(request.phone))
            {
                errors.Add("Phone number must start with 010, 011, or 012 and be exactly 11 digits long");
            }

            if (!new Regex(@"^\d{4}(-\d{4})?$").IsMatch(request.academicYear))
            {
                errors.Add("Academic year must be in the format 'YYYY' or 'YYYY-YYYY'");
            }

            if (request.dateOfBirth != default)
            {
                var dateOfBirth = request.dateOfBirth;
                var calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
                {
                    calculatedAge--;
                }

                // Check if calculated age matches the age provided
              
                if (request.age != calculatedAge)
                {
                    errors.Add("Age does not match Date of Birth");
                }

                // Role-based age validation
                if (request.RoleId == 1) 
                {
                    if (calculatedAge < 14 || calculatedAge > 25)
                    {
                        errors.Add("For students, age must be between 14 and 25");
                    }
                }
                else if (request.RoleId == 2) 
                {
                    if (calculatedAge < 25 || calculatedAge > 60)
                    {
                        errors.Add("For teachers, age must be between 25 and 60");
                    }
                }
            }

            return errors;
        }


        private ICollection<string> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model);

            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults.Select(vr => vr.ErrorMessage).ToList();
        }

        public static class ValidationHelper
        {
            public static ICollection<ValidationResult> ConvertToValidationResults(IEnumerable<string> errorMessages)
            {
                var validationResults = new List<ValidationResult>();
                foreach (var message in errorMessages)
                {
                    validationResults.Add(new ValidationResult(message));
                }
                return validationResults;
            }
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<GeneralResponse>> GetAllRoles()
        {
            try
            {
                var roles = await _authProvider.AuthenticationRepo.GetAllRoles();
                return Ok(GeneralResponse.Create(HttpStatusCode.OK, roles, "Roles retrieved successfully"));
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    GeneralResponse.Create(HttpStatusCode.InternalServerError, null, "An error occurred, please try again later"));
            }
        }

        [HttpPost("validate-user-role")]
        public async Task<IActionResult> ValidateUserRole(userRequest request)
        {
            var response = await _authProvider.AuthenticationRepo.ValidateUserRole(request);

            if (response == null || !response.IsValid)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    isSuccess = false,
                    result = (object)null,
                    errors = new
                    {
                        message = "Invalid user or role."
                    }
                });
            }

            return Ok(new
            {
                statusCode = 200,
                isSuccess = true,
                result = response,
                errors = (object)null
            });
        }




    }
}

