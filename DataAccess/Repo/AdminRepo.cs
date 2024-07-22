using DataAccess.IRepo;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Request;
using Models.DTOs.Response;
using Models.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo
{
    public class AdminRepo : IAdminRepo
    {
        private readonly ApplicationDBContext _context;
        public AdminRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        #region student
       
        public async Task<List<StudentResponse>> GetAllStudents()
        {
            try
            {
                List<StudentResponse> StudentResponses = new List<StudentResponse>();
                var students = await _context.students
                    .ToListAsync();
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        StudentResponses.Add(new StudentResponse
                        {
                            id = student.Id,
                            name = student.Name,
                        });
                    }
                    return StudentResponses;
                }
                else
                {
                    return StudentResponses;
                }

            }
            catch { return new List<StudentResponse>(); }
        }

        public async Task<StudentResponseById> GetStudentById(studentReq Request)
        {
            try
            {
                StudentResponseById res = new StudentResponseById();
                var student = await _context.students

                    .Where(h => h.Id == Request.id).FirstOrDefaultAsync();
                if (student != null)
                {
                    res.id = student.Id;
                    res.name = student.Name;



                    return res;
                }
                else
                {
                    return null;
                }

            }
            catch { return null; }
        }

        public async Task<int> SaveStudent(SaveStudentRequest req)
        {
            try
            {
                if (req.StudentId == null || req.StudentId<= 0)
                {
                    return await AddStudent(req);
                }
                else
                {
                    return await EditStudent(req);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in SaveStudent: {ex.Message}");
                return -1;
            }
        }
        private async Task<int> AddStudent(SaveStudentRequest Req)
        {
            try
            {
                Student students = new Student();
                students.Name = Req.StudentName;
                students.UserId = Req.userId;
                students.createdAt = DateTime.Now;
                students.createdBy = Req.userId;
                students.isDeleted = false;
                students.isEnabled = true;


                _context.students.Add(students);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        private async Task<int> EditStudent(SaveStudentRequest Req)
        {
            try
            {
                var student = await _context.students
                                            .Where(s => s.Id == Req.StudentId)
                                            .FirstOrDefaultAsync();
                if (student != null)
                {
                    student.Name = Req.StudentName;
                    student.UserId = Req.userId;
                    student.modifiedAt = DateTime.Now;
                    student.modifiedBy = Req.userId;
                    _context.Entry(student).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {

                return -1;
            }
        }



        #endregion


        #region teacher


        public async Task<List<TeacherResponse>> GetAllTeachers()
        {
            try
            {
                List<TeacherResponse> TeacherResponses = new List<TeacherResponse>();
                var teachers = await _context.teachers
                    .ToListAsync();
                if (teachers != null)
                {
                    foreach (var teacher in teachers)
                    {
                        TeacherResponses.Add(new TeacherResponse
                        {
                            id = teacher.Id,
                            name = teacher.Name,
                        });
                    }
                    return TeacherResponses;
                }
                else
                {
                    return TeacherResponses;
                }

            }
            catch { return new List<TeacherResponse>(); }
        }

        public async Task<TeacherResponseById> GetTeacherById(TeacherRequest Request)
        {
            try
            {
                TeacherResponseById res = new TeacherResponseById();
                var teacher = await _context.teachers

                    .Where(h => h.Id == Request.id).FirstOrDefaultAsync();
                if (teacher != null)
                {
                    res.id = teacher.Id;
                    res.name = teacher.Name;



                    return res;
                }
                else
                {
                    return null;
                }

            }
            catch { return null; }
        }


        public async Task<int> SaveTeacher(SaveTeacherRequest req)
        {
            try
            {
                if (req.id == null || req.id <= 0)
                {
                    return await AddTeacher(req);
                }
                else
                {
                    return await EditTeacher(req);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in SaveTeacher: {ex.Message}");
                return -1;
            }
        }
        private async Task<int> AddTeacher(SaveTeacherRequest Req)
        {
            try
            {
                Teacher teachers = new Teacher();
                teachers.Name = Req.name;
                teachers.UserId = Req.userId;
                teachers.createdAt = DateTime.Now;
                teachers.createdBy = Req.userId;
                teachers.isDeleted = false;
                teachers.isEnabled = true;


                _context.teachers.Add(teachers);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        private async Task<int> EditTeacher(SaveTeacherRequest Req)
        {
            try
            {
                var teacher = await _context.teachers
                                            .Where(s => s.Id == Req.id)
                                            .FirstOrDefaultAsync();
                if (teacher != null)
                {
                    teacher.Name = Req.name;
                    teacher.UserId = Req.userId;
                    teacher.modifiedAt = DateTime.Now;
                    teacher.modifiedBy = Req.userId;
                    _context.Entry(teacher).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {

                return -1;
            }
        }

        #endregion

        #region subject

        //create subject
        public async Task<List<GetSubjectsDropdown>> GetSubjectsDropdown(GetSubjectsByYearRequest request)
        {
            try
            {
                List<GetSubjectsDropdown> subjectsDropdown = new List<GetSubjectsDropdown>();

                var subjects = await _context.subjects
                                             .Where(s => s.AcademicYear == request.AcademicYear && s.isDeleted == false)
                                             .ToListAsync();

                if (subjects != null && subjects.Count > 0)
                {
                    foreach (var subject in subjects)
                    {
                        subjectsDropdown.Add(new GetSubjectsDropdown()
                        {
                            id = subject.Id,
                            name = subject.Name
                        });
                    }
                }
                return subjectsDropdown;
            }
            catch
            {
                return new List<GetSubjectsDropdown>();
            }
        }

        public async Task<int> SaveSubject(SaveSubjectRequest req)
        {
            try
            {
                if (req.id == null || req.id <= 0)
                {
                    return await AddSubject(req);
                }
                else
                {
                    return await EditSubject(req);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in SaveSubject: {ex.Message}");
                return -1;
            }
        }

        public async Task<bool> DeleteSubject(SubjectRequest subjectRequest)
        {
            if (subjectRequest == null || subjectRequest.id <= 0)
            {
                Console.WriteLine("Invalid subject request data.");
                return false;
            }

            try
            {
                var subject = await _context.subjects.FindAsync(subjectRequest.id);

                if (subject != null && subject.isDeleted == false)
                {
                    subject.isDeleted = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {

                    Console.WriteLine($"Subject with id {subjectRequest.id} not found or already deleted.");
                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while deleting subject: {ex.Message}");
                return false;
            }
        }

        public async Task<SubjectResponse?> GetSubjectById(SubjectRequest subjectRequest)
        {
            try
            {
                SubjectResponse res = new SubjectResponse();
                var subject = await _context.subjects.Where(H => H.Id == subjectRequest.id && H.isDeleted == false).FirstOrDefaultAsync();
                if (subject != null)
                {
                    res.id = subject.Id;
                    res.name = subject.Name;
                    res.AcademicYear = subject.AcademicYear;
                    return res;
                }
                else
                {
                    return null;
                }

            }
            catch { return null; }
        }
        private async Task<int> AddSubject(SaveSubjectRequest saveSubjectsReq)
        {
            try
            {
                Subjects subjects = new Subjects();
                subjects.Name = saveSubjectsReq.name;
                subjects.AcademicYear = saveSubjectsReq.AcademicYear;
                subjects.createdAt = DateTime.Now;
                subjects.createdBy = saveSubjectsReq.userId;
                subjects.isDeleted = false;
                subjects.isEnabled = true;


                _context.subjects.Add(subjects);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        private async Task<int> EditSubject(SaveSubjectRequest saveSubjectsReq)
        {
            try
            {
                var subject = await _context.subjects
                                            .Where(s => s.Id == saveSubjectsReq.id)
                                            .FirstOrDefaultAsync();
                if (subject != null)
                {
                    subject.Name = saveSubjectsReq.name;
                    subject.AcademicYear = saveSubjectsReq.AcademicYear;
                    subject.modifiedAt = DateTime.Now;
                    subject.modifiedBy = saveSubjectsReq.userId;
                    _context.Entry(subject).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {

                return -1;
            }
        }
        #endregion
    }
}
