using Models.DTOs.Request;
using Models.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepo
{
    public interface IAdminRepo
    {

        Task<List<GetSubjectsDropdown>> GetSubjectsDropdown(GetSubjectsByYearRequest request);
        Task<int> SaveSubject(SaveSubjectRequest req);
        Task<SubjectResponse?> GetSubjectById(SubjectRequest subjectRequest);
        Task<bool> DeleteSubject(SubjectRequest SubjectRequest);

        Task<List<TeacherResponse>> GetAllTeachers();
        Task<TeacherResponseById> GetTeacherById(TeacherRequest Request);
        Task<int> SaveTeacher(SaveTeacherRequest req);
        Task<List<StudentResponse>> GetAllStudents();
        Task<StudentResponseById> GetStudentById(studentReq Request);
        Task<int> SaveStudent(SaveStudentRequest req);



    }
}
