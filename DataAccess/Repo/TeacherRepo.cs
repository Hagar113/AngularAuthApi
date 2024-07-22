using DataAccess.IRepo;
using Models.DTOs.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTOs.Response;
using Models.DTOs.Request;
using Models.models;

namespace DataAccess.Repo
{
    public class TeacherRepo: ITeacherRepo
    {
        private readonly ApplicationDBContext _context;

        public TeacherRepo(ApplicationDBContext context)
        {
            _context = context;
        }
       
        
        


        public async Task<int> SaveTeacherSubjectAsync(SaveSubjectTeacherRequest request)
        {
            try
            {
                TeacherSubjects teacherSubject = new TeacherSubjects
                {
                    TeacherId = request.teacherId,
                    SubjectId = request.subjectId,
                    createdAt = DateTime.Now,
                    createdBy = request.userId,
                    isDeleted = false,
                    isEnabled = true
                };

                _context.TeacherSubjects.Add(teacherSubject);
                await _context.SaveChangesAsync();

                return 1;
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ
                return -1;
            }
        }

    }
}
