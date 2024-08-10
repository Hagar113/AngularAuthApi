using DataAccess.IRepo;
using Infrastructure.helpers;
using Infrastructure.helpers.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs.BaseRequest;
using Models.DTOs.Request;
using Models.DTOs.Response;
using Models.models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccess.Repo
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly ApplicationDBContext _context;

        public AuthenticationRepo(ApplicationDBContext context)
        {
            _context = context;
        }





        public async Task<bool> UserRegister(BaseRequestHeader baseRequest)
        {
            try
            {
                RegisterRequest registerRequest = Deserialization.Deserialize<RegisterRequest>(baseRequest.data.ToString());

                // Validate the RegisterRequest object directly
                if (!CheckRequestedObj(registerRequest))
                {
                    Console.WriteLine("Invalid registration request");
                    return false;
                }

                Encryption encryption = new Encryption();
                string encryptedPassword = encryption.Encrypt(registerRequest.password);

                var role = await _context.roles.FindAsync(registerRequest.RoleId);
                if (role == null)
                {
                    return false;
                }

                Users newUser = new Users
                {
                    Username = registerRequest.userName,
                    Name = registerRequest.firstName,
                    Age = registerRequest.age,
                    Email = registerRequest.email,
                    Password = encryptedPassword,
                    phone = registerRequest.phone,
                    schoolYear = registerRequest.academicYear,
                   roleId = role.id
                };

                await _context.users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                switch (role.code)
                {
                    case "STUDENT":
                        Student newStudent = new Student
                        {
                            Name = registerRequest.firstName,
                            UserId = newUser.Id
                        };
                        await _context.students.AddAsync(newStudent);
                        Console.WriteLine("Student saved successfully");
                        break;

                    case "TEACHER":
                        Teacher newTeacher = new Teacher
                        {
                            Name = registerRequest.firstName,
                            UserId = newUser.Id
                        };
                        await _context.teachers.AddAsync(newTeacher);
                        Console.WriteLine("Teacher saved successfully");
                        break;

                    case "ADMIN":
                        // Add additional handling for the ADMIN role if needed
                        Console.WriteLine("Admin role detected");
                        // Since it's already added to Users table, no need to add again
                        break;

                    default:
                        Console.WriteLine("Unknown role code: " + role.code);
                        return false;
                }


                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UserRegister: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                    Console.WriteLine("Inner Stack Trace: " + ex.InnerException.StackTrace);
                }

                return false;
            }
        }





        public bool CheckRequestedObj(RegisterRequest registerRequest)
        {
            // Simplified validation logic
            return !string.IsNullOrEmpty(registerRequest.userName) &&
                   !string.IsNullOrEmpty(registerRequest.phone) &&
                   !string.IsNullOrEmpty(registerRequest.email) &&
                   !string.IsNullOrEmpty(registerRequest.firstName) &&
                   registerRequest.age > 0 &&
                   !string.IsNullOrEmpty(registerRequest.password) &&
                   registerRequest.RoleId != 0;
        }



        public async Task<List<RolesResponse>> GetAllRoles()
        {
            try
            {
                List<RolesResponse> RolesResponses = new List<RolesResponse>();
                var roles = await _context.roles
                    .ToListAsync();
                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        RolesResponses.Add(new RolesResponse
                        {
                          
                            name=role.Name,
                            id=role.id
                        });
                    }
                    return RolesResponses;
                }
                else
                {
                    return RolesResponses;
                }

            }
            catch { return new List<RolesResponse>(); }
        }



        //public bool CheckRequestedObj(RegisterRequest registerRequest)
        //{
        //    // Simplified validation logic
        //    return !string.IsNullOrEmpty(registerRequest.userName) &&
        //           !string.IsNullOrEmpty(registerRequest.phone) &&
        //           !string.IsNullOrEmpty(registerRequest.email) &&
        //           !string.IsNullOrEmpty(registerRequest.firstName) &&
        //           registerRequest.age > 0 &&
                   
        //           !string.IsNullOrEmpty(registerRequest.password) &&
        //           registerRequest.RoleId != 0;
        //}

        public async Task<bool> checkIfEmailOrPhoneOrUsernameExists(string _email, string _phone)
        {
            return await _context.users.AnyAsync(h => h.Email == _email || h.phone == _phone);
        }
        public string CheckPasswordStrength(string _password)
        {
            StringBuilder sb = new StringBuilder();

            if (_password.Length < 6)
                sb.Append("The password must be at least 6 characters long" + Environment.NewLine);
            if (!(Regex.IsMatch(_password, "[a-z]")     // Check for at least one lowercase letter
                 && Regex.IsMatch(_password, "[A-Z]")    // Check for at least one uppercase letter
                 && Regex.IsMatch(_password, "[0-9]")    // Check for at least one digit
                 && Regex.IsMatch(_password, "[^a-zA-Z0-9]"))) // Check for at least one special character
            {
                sb.Append("The password must contain at least one lowercase letter, one uppercase letter, one number, and one special character." + Environment.NewLine);
            }


            return sb.ToString();
        }

        public async Task<LoginResponse> Login(BaseRequestHeader baseRequest)
        {
            try
            {
                // Deserialize the LoginRequest from the baseRequest data
                LoginRequest loginRequest = Deserialization.Deserialize<LoginRequest>(baseRequest.data.ToString());

                var user = await _context.users
                            .Include(u => u.Role) // Include Role details
                            .FirstOrDefaultAsync(h => h.Email == loginRequest.email_phone || h.phone == loginRequest.email_phone);

                if (user == null)
                {
                    return null;
                }

                Encryption encryption = new Encryption();
                if (encryption.Encrypt(loginRequest.password) != user.Password)
                {
                    return null;
                }

                LoginResponse loginResponse = new LoginResponse
                {
                    token = GenerateJwtToken(user),
                    UserDto = new UserDto
                    {
                        Id = user.Id,
                        UserName = user.Username,
                        Email = user.Email,
                        phone = user.phone,
                        name = user.Name,
                        Role = user.Role != null ? new RoleDto
                        {
                            Id = user.Role.id,
                            Name = user.Role.Name
                        } : null
                    }
                };

                user.Token = loginResponse.token;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return loginResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }




        public async Task<Users> GetUserById(int userId)
        {
            return await _context.users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }




        private string GenerateJwtToken(Users user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("InTheNameOfAllah...");

            // Prepare claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.MobilePhone, user.phone),
        new Claim("Name", user.Name),
        new Claim("age", user.Age.ToString() ?? string.Empty),
        new Claim("schoolYear", user.schoolYear.ToString() ?? string.Empty),
    };

           
            if (user.Role != null)
            {
                claims.Add(new Claim("Role", user.Role.Name)); 
            }
            else
            {
                claims.Add(new Claim("Role", string.Empty));
            }

            // Add isActive claim if user.isActive has value
            if (user.isActive.HasValue)
            {
                claims.Add(new Claim("isActive", user.isActive.Value.ToString()));
            }
            else
            {
                claims.Add(new Claim("isActive", string.Empty)); 
            }

            // Create identity and token
            var identity = new ClaimsIdentity(claims);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials,
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }


        public async Task<bool> IsRoleAssignedToUser(int userId, string roleCode)
        {
            var user = await _context.users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user != null && user.Role != null && user.Role.code == roleCode;
        }



        public async Task<PagesResponse> ValidateUserRole(userRequest request)
        {
            try
            {
                var user = await _context.users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == request.userId);

                if (user == null || user.Role == null || user.Role.id != request.roleId)
                {
                    return new PagesResponse
                    {
                        IsValid = false,
                        Pages = null
                    };
                }

                var rolePages = await _context.rolepage
                    .Include(rp => rp.pages) 
                    .Where(rp => rp.RoleId == request.roleId)
                    .Select(rp => new PageDto
                    {
                        PageId = rp.PageId,
                        PageName = rp.pages.name,
                        PagePath = rp.pages.path
                    })
                    .ToListAsync();

                return new PagesResponse
                {
                    IsValid = true,
                    Pages = rolePages
                };
            }
            catch (Exception ex)
            {
               
                return new PagesResponse
                {
                    IsValid = false,
                    Pages = null
                };
            }
        }


    }

}
