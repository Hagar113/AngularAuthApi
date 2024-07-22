using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace Models.DTOs.Response
    {
        public class LoginResponse
        {
            public string? Token { get; set; }
            public UserDto UserDto { get; set; }
        }

        public class UserDto
        {
            public int Id { get; set; }
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public string? phone { get; set; }
            public string? name { get; set; }
            public RoleDto Role { get; set; }
        }

        public class RoleDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }



