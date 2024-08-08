﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class SaveUserRequest
    {
        public int? userId { get; set; } 

        public string userName { get; set; } 

        public string firstName { get; set; } 

        public string email { get; set; } 

        public string phone { get; set; } 

        public int age { get; set; } 

        public string password { get; set; } 

        public string academicYear { get; set; } 

        public DateTime dateOfBirth { get; set; } 

        public int roleId { get; set; }
    }
}