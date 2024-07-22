using Models.DTOs.Request;
using Models.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepo
{
    public interface ITeacherRepo
    {

        Task<int> SaveTeacherSubjectAsync(SaveSubjectTeacherRequest request);



    }
}
