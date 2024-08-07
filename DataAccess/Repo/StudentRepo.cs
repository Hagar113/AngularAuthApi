using DataAccess.IRepo;
using Models.DTOs.Request;
using Models.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Response;


namespace DataAccess.Repo
{
    public class StudentRepo : IStudentRepo
    {
        private readonly ApplicationDBContext _context;
        
        public StudentRepo(ApplicationDBContext context)
        {
            _context = context;
        }

       


        ///////////////////////////////////////////////////////////////
        //public async Task<int> SaveStudentSubjectTeacherAsync(SaveStudentSubjectTeacher request)
        //{
        //    try
        //    {
                
        //        var existingEntry = await _context.studentSubjects
        //            .FirstOrDefaultAsync(ss => ss.StudentId == request.studentId && ss.SubjectId == request.subjectId);

        //        if (existingEntry != null)
        //        {
        //            return -2; 
        //        }

              
        //        StudentSubjects studentSubject = new StudentSubjects
        //        {
        //            StudentId = request.studentId,
        //            SubjectId = request.subjectId,
        //            TeacherId = request.teacherId,
        //            createdAt = DateTime.Now,
        //            createdBy =request.userId, // Assuming the student is creating this record
        //            isDeleted = false,
        //            isEnabled = true
        //        };

        //        _context.studentSubjects.Add(studentSubject);
        //        await _context.SaveChangesAsync();
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception
        //        return -1;
        //    }
        //}

    }

   
}




